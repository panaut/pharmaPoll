﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Questionnaire.Data.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Questionnaire.Data.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALTER TABLE [dbo].[ElementBases_QuestionBase]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.QuestionBases_dbo.SelectBases_ComplexMatrixBase_ComplexMatrixBaseId] FOREIGN KEY([ComplexMatrixBaseId])
        ///REFERENCES [dbo].[SelectBases_ComplexMatrixBase] ([Id])
        ///GO
        ///
        ///ALTER TABLE [dbo].[ElementBases_QuestionBase] NOCHECK CONSTRAINT [FK_dbo.QuestionBases_dbo.SelectBases_ComplexMatrixBase_ComplexMatrixBaseId]
        ///GO.
        /// </summary>
        internal static string ForeignKey_ComplexMatrixBaseQuestionBase {
            get {
                return ResourceManager.GetString("ForeignKey_ComplexMatrixBaseQuestionBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALTER TABLE [dbo].[MatrixRows]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.MatrixRows_dbo.ComplexMatrixBases_ComplexMatrix_ComplexMatrixId] FOREIGN KEY([ComplexMatrixId])
        ///REFERENCES [dbo].[ComplexMatrixBases_ComplexMatrix] ([Id])
        ///GO
        ///
        ///ALTER TABLE [dbo].[MatrixRows] NOCHECK CONSTRAINT [FK_dbo.MatrixRows_dbo.ComplexMatrixBases_ComplexMatrix_ComplexMatrixId]
        ///GO.
        /// </summary>
        internal static string ForeignKey_ComplexMatrixMatrixRows {
            get {
                return ResourceManager.GetString("ForeignKey_ComplexMatrixMatrixRows", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALTER TABLE [dbo].[ElementBases]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ElementBases_dbo.ElementBases_ElementContainer_ElementContainerId] FOREIGN KEY([ElementContainerId])
        ///REFERENCES [dbo].[ElementBases_ElementContainer] ([Id])
        ///GO
        ///
        ///ALTER TABLE [dbo].[ElementBases] NOCHECK CONSTRAINT [FK_dbo.ElementBases_dbo.ElementBases_ElementContainer_ElementContainerId]
        ///GO.
        /// </summary>
        internal static string ForeignKey_ElementBaseElementContainer {
            get {
                return ResourceManager.GetString("ForeignKey_ElementBaseElementContainer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALTER TABLE [dbo].[QuestionBases_MultipleTextItem]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.QuestionBases_MultipleTextItem_dbo.QuestionBases_MultipleText_MultipleTextQuestionId] FOREIGN KEY([MultipleTextQuestionId])
        ///REFERENCES [dbo].[QuestionBases_MultipleText] ([Id])
        ///GO
        ///
        ///ALTER TABLE [dbo].[QuestionBases_MultipleTextItem] NOCHECK CONSTRAINT [FK_dbo.QuestionBases_MultipleTextItem_dbo.QuestionBases_MultipleText_MultipleTextQuestionId]
        ///GO.
        /// </summary>
        internal static string ForeignKey_MultipleTextMultipleTextItem {
            get {
                return ResourceManager.GetString("ForeignKey_MultipleTextMultipleTextItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALTER TABLE [dbo].[Choices]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Choices_dbo.QuestionBases_PlainMatrix_PlainMatrixId] FOREIGN KEY([PlainMatrixId])
        ///REFERENCES [dbo].[QuestionBases_PlainMatrix] ([Id])
        ///GO
        ///
        ///ALTER TABLE [dbo].[Choices] NOCHECK CONSTRAINT [FK_dbo.Choices_dbo.QuestionBases_PlainMatrix_PlainMatrixId]
        ///GO.
        /// </summary>
        internal static string ForeignKey_PlainMatrixChoices {
            get {
                return ResourceManager.GetString("ForeignKey_PlainMatrixChoices", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALTER TABLE [dbo].[MatrixRows]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.MatrixRows_dbo.QuestionBases_PlainMatrix_PlainMatrixId] FOREIGN KEY([PlainMatrixId])
        ///REFERENCES [dbo].[QuestionBases_PlainMatrix] ([Id])
        ///GO
        ///
        ///ALTER TABLE [dbo].[MatrixRows] NOCHECK CONSTRAINT [FK_dbo.MatrixRows_dbo.QuestionBases_PlainMatrix_PlainMatrixId]
        ///GO.
        /// </summary>
        internal static string ForeignKey_PlainMatrixMatrixRows {
            get {
                return ResourceManager.GetString("ForeignKey_PlainMatrixMatrixRows", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALTER TABLE [dbo].[Choices]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Choices_dbo.QuestionBases_Rating_RatingId] FOREIGN KEY([RatingId])
        ///REFERENCES [dbo].[QuestionBases_Rating] ([Id])
        ///GO
        ///
        ///ALTER TABLE [dbo].[Choices] NOCHECK CONSTRAINT [FK_dbo.Choices_dbo.QuestionBases_Rating_RatingId]
        ///GO.
        /// </summary>
        internal static string ForeignKey_RatingChoices {
            get {
                return ResourceManager.GetString("ForeignKey_RatingChoices", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ALTER TABLE [dbo].[Choices]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Choices_dbo.QuestionBases_SelectBases_QuestionId] FOREIGN KEY([QuestionId])
        ///REFERENCES [dbo].[QuestionBases_SelectBases] ([Id])
        ///GO
        ///
        ///ALTER TABLE [dbo].[Choices] NOCHECK CONSTRAINT [FK_dbo.Choices_dbo.QuestionBases_SelectBases_QuestionId]
        ///GO.
        /// </summary>
        internal static string ForeignKey_SelectQuestionBaseChoices {
            get {
                return ResourceManager.GetString("ForeignKey_SelectQuestionBaseChoices", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TRIGGER dbo.DeleteChoicesOfPlainMatrix
        ///   ON  dbo.QuestionBases_PlainMatrix
        ///   FOR DELETE
        ///AS 
        ///BEGIN
        ///	DELETE dbo.Choices WHERE PlainMatrixId IN (SELECT Id from deleted)
        ///END.
        /// </summary>
        internal static string Trigger_DeleteChoicesOfPlainMatrix {
            get {
                return ResourceManager.GetString("Trigger_DeleteChoicesOfPlainMatrix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TRIGGER dbo.DeleteChoicesOfRating
        ///   ON  dbo.QuestionBases_Rating
        ///   FOR DELETE
        ///AS 
        ///BEGIN
        ///	DELETE dbo.Choices WHERE RatingId IN (SELECT Id from deleted)
        ///END.
        /// </summary>
        internal static string Trigger_DeleteChoicesOfRating {
            get {
                return ResourceManager.GetString("Trigger_DeleteChoicesOfRating", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TRIGGER dbo.DeleteChoicesOfSelectBase
        ///   ON  dbo.QuestionBases_SelectBases
        ///   FOR DELETE
        ///AS 
        ///BEGIN
        ///	DELETE dbo.Choices WHERE QuestionId IN (SELECT Id from deleted)
        ///END.
        /// </summary>
        internal static string Trigger_DeleteChoicesOfSelectBase {
            get {
                return ResourceManager.GetString("Trigger_DeleteChoicesOfSelectBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TRIGGER [dbo].[DeleteComplexMatrixColumns]
        ///   ON  [dbo].[SelectBases_ComplexMatrixBase]
        ///   FOR DELETE
        ///AS 
        ///BEGIN
        ///
        ///	DELETE dbo.ElementBases WHERE Id IN (SELECT Id FROM ElementBases_QuestionBase WHERE ComplexMatrixBaseId IN (SELECT Id from deleted))
        ///END.
        /// </summary>
        internal static string Trigger_DeleteComplexMatrixColumns {
            get {
                return ResourceManager.GetString("Trigger_DeleteComplexMatrixColumns", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TRIGGER [dbo].[DeleteElementBasesForContainer]
        ///   ON  [dbo].[ElementBases_ElementContainer]
        ///   FOR DELETE
        ///AS 
        ///BEGIN
        ///	DELETE ElementBases WHERE ElementContainerId IN (SELECT Id FROM deleted)
        ///
        ///END
        ///
        ///GO
        ///
        ///ALTER TABLE [dbo].[ElementBases_ElementContainer] ENABLE TRIGGER [DeleteElementBasesForContainer]
        ///GO.
        /// </summary>
        internal static string Trigger_DeleteElementBasesForContainer {
            get {
                return ResourceManager.GetString("Trigger_DeleteElementBasesForContainer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TRIGGER dbo.DeleteMatrixRowsOfComplexMatrix
        ///   ON  dbo.ComplexMatrixBases_ComplexMatrix
        ///   FOR DELETE
        ///AS 
        ///BEGIN
        ///	DELETE dbo.MatrixRows WHERE ComplexMatrixId IN (SELECT Id from deleted)
        ///END.
        /// </summary>
        internal static string Trigger_DeleteMatrixRowsOfComplexMatrix {
            get {
                return ResourceManager.GetString("Trigger_DeleteMatrixRowsOfComplexMatrix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TRIGGER dbo.DeleteMatrixRowsOfPlainMatrix
        ///   ON  dbo.QuestionBases_PlainMatrix
        ///   FOR DELETE
        ///AS 
        ///BEGIN
        ///	DELETE dbo.MatrixRows WHERE PlainMatrixId IN (SELECT Id from deleted)
        ///END.
        /// </summary>
        internal static string Trigger_DeleteMatrixRowsOfPlainMatrix {
            get {
                return ResourceManager.GetString("Trigger_DeleteMatrixRowsOfPlainMatrix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TRIGGER [dbo].[DeleteMultipleTextItems]
        ///   ON  [dbo].[QuestionBases_MultipleText]
        ///   FOR DELETE
        ///AS 
        ///BEGIN
        ///	DELETE dbo.ElementBases WHERE Id IN (SELECT Id FROM QuestionBases_MultipleTextItem WHERE MultipleTextQuestionId IN (SELECT Id from deleted))
        ///END.
        /// </summary>
        internal static string Trigger_DeleteMultipleTextItems {
            get {
                return ResourceManager.GetString("Trigger_DeleteMultipleTextItems", resourceCulture);
            }
        }
    }
}
