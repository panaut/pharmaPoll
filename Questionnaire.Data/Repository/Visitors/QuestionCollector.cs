using Questionnaire.Data.Model;
using Questionnaire.Data.Model.QuestionDefinition;
using System.Collections.Generic;

namespace Questionnaire.Data.Repository.Visitors
{
    internal class QuestionCollector : VisitorBase
    {
        //private object currentIndexLock = new object();
        private int currentIndex = 1;

        private ElementContainer currentContainer = null;

        public IEnumerable<ElementContainer> ElementContainers { get; private set; }

        public IEnumerable<QuestionBase> Questions { get; private set; }

        public IEnumerable<QuestionBase> QuestionsToAdd { get; private set; }

        public IEnumerable<QuestionBase> QuestionsToUpdate { get; private set; }

        public QuestionCollector()
        {
            this.Questions = new List<QuestionBase>();
            this.QuestionsToAdd = new List<QuestionBase>();
            this.QuestionsToUpdate = new List<QuestionBase>();
            this.ElementContainers = new List<ElementContainer>();
        }

        //private int GetNextIndex()
        //{
        //    int retVal = 0;

        //    lock(currentIndexLock)
        //    {
        //        retVal = this.currentIndex++;
        //    }

        //    return retVal;
        //}

        public override void Visit(QuestionBase question)
        {
            question.ElementContainerUId = this.currentContainer.ContainerUID;
            ((List<QuestionBase>)Questions).Add(question);

            //question.PositionWithinContainer = this.GetNextIndex();
            question.PositionWithinContainer = this.currentIndex++;

            if (question.Id == 0)
            {
                ((List<QuestionBase>)QuestionsToAdd).Add(question);
            }
            else
            {
                ((List<QuestionBase>)QuestionsToUpdate).Add(question);
            }
        }

        public override void Visit(Panel panel)
        {
            //panel.PositionWithinContainer = this.GetNextIndex();
            panel.PositionWithinContainer = this.currentIndex++;
        }

        public override void VisitingNewContainer(ElementContainer container)
        {
            this.currentContainer = container;

            // reset indexCounter
            this.currentIndex = 1;

            ((List<ElementContainer>)this.ElementContainers).Add(container);
        }
    }
}
