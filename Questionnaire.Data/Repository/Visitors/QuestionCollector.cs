using Questionnaire.Data.Model;
using System;
using Questionnaire.Data.Model.QuestionDefinition;
using Questionnaire.Data.Repository.Visitors;
using System.Collections.Generic;

namespace Questionnaire.Data.Repository.Visitors
{
    internal class QuestionCollector : VisitorBase
    {
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

        public override void Visit(QuestionBase question)
        {
            question.ElementContainerUId = this.currentContainer.ContainerUID;
            ((List<QuestionBase>)Questions).Add(question);

            if (question.Id == 0)
            {
                ((List<QuestionBase>)QuestionsToAdd).Add(question);
            }
            else
            {
                ((List<QuestionBase>)QuestionsToUpdate).Add(question);
            }
        }

        public override void VisitingNewContainer(ElementContainer container)
        {
            this.currentContainer = container;
            ((List<ElementContainer>)this.ElementContainers).Add(container);
        }
    }
}
