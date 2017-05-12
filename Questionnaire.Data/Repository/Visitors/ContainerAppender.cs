using Questionnaire.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Data.Repository.Visitors
{
    // ToDo: IC - Refactor this class into visitor
    internal class ContainerAppender
    {
        //private object uidLock = new object();
        private int uid = 1;

        //private object currentIndexLock = new object();
        private int currentIndex = 1;

        public IDictionary<int?, ElementContainer> dbElementContainerDict { get; set; }
            = new Dictionary<int?, ElementContainer>();

        private SurveyModelContext context = null;

        //private int GetNextUid()
        //{
        //    int nextUid = 0;

        //    lock (uidLock)
        //    {
        //        nextUid = uid++;
        //    }

        //    return nextUid;
        //}

        //private int GetNextIndex()
        //{
        //    int retVal = 0;

        //    lock (currentIndexLock)
        //    {
        //        retVal = this.currentIndex++;
        //    }

        //    return retVal;
        //}

        public void AppendContainers(SurveyModelContext context, Survey surveyDb, Survey newSurvey)
        {
            this.context = context;

            foreach (var page in newSurvey.elements.OfType<Page>())
            {
                //page.ContainerUID = this.GetNextUid();
                page.ContainerUID = this.uid++;
                //page.PositionWithinContainer = this.GetNextIndex();
                page.PositionWithinContainer = this.currentIndex++;

                Page pageDb = null;

                // Is it new page?
                if (page.Id == 0)
                {
                    // Create a new Object in DbContext
                    pageDb = context.Elements.Create<Page>();
                    surveyDb.elements.Add(pageDb);
                }
                else
                {
                    pageDb = (Page)context.Elements.Find(page.Id);
                }

                // Copy values from new page
                context.Entry(pageDb).CurrentValues.SetValues(page);
                pageDb.SurveyId = surveyDb.Id;
                pageDb.ContainerUID = page.ContainerUID;

                this.dbElementContainerDict.Add(pageDb.ContainerUID, pageDb);

                SyncContainerElements(pageDb, page);
            }
        }

        public void SyncContainerElements(ElementContainer containerDb, ElementContainer container)
        {
            foreach (var panel in container.elements.OfType<Panel>())
            {
                //panel.ContainerUID = this.GetNextUid();
                panel.ContainerUID = this.uid++;

                Panel panelDb = null;

                // Is it new panel?
                if (panel.Id == 0)
                {
                    // Create a new Object in DbContext
                    panelDb = context.Elements.Create<Panel>();
                    containerDb.elements.Add(panelDb);
                }
                else
                {
                    panelDb = (Panel)context.Elements.Find(panel.Id);
                }

                // Copy values from new panel
                context.Entry(panelDb).CurrentValues.SetValues(panel);

                panelDb.SurveyId = containerDb.SurveyId;
                panelDb.ElementContainerUId = containerDb.ContainerUID;

                // If it is an existing panel, restore foreign key...
                if (panel.Id != 0)
                {
                    panelDb.ElementContainerId = containerDb.Id;
                }

                panelDb.ContainerUID = panel.ContainerUID;

                this.dbElementContainerDict.Add(panelDb.ContainerUID, panelDb);

                SyncContainerElements(panelDb, panel);
            }
        }
    }
}
