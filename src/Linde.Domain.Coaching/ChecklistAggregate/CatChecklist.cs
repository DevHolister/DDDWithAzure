using Linde.Domain.Coaching.ChecklistAggregate.Entities;
using Linde.Domain.Coaching.ChecklistAggregate.ValueObjects;
using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.Questionsaggregate.Entities;
using Linde.Domain.Coaching.Questionsaggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Domain.Coaching.ChecklistAggregate
{
    public sealed class CatChecklist : Entity<ChecklistId>, IAggregateRoot
    {
        private readonly List<ChecklistsQuestions> _ChecklistsQuestions = new();
        public IReadOnlyList<ChecklistsQuestions> ChecklistsQuestions => _ChecklistsQuestions.AsReadOnly();
        public string Name { get; set; }
        public string Description { get; set; }
        private CatChecklist(ChecklistId checklistId, string name, string description)
        {
            Id = checklistId;
            Name = name;
            Description = description;
            Visible = true;
        }
        public static CatChecklist Create(string name, string description)
        {
            return new(
                   ChecklistId.CreateUnique(),
                   name,
                   description);
        }
        public void UpdateChecklist(string name, string description)
        {
            Name = name;
            Description = description;
        }
        private CatChecklist() { }
        public void AddQuestion(ChecklistsQuestions question)
        {
                _ChecklistsQuestions.Add(question);
        }
        public void UpdateQuestion(List<ChecklistsQuestions> questions)
        {
            _ChecklistsQuestions.RemoveAll(x => !questions.Contains(x));
            questions.ForEach(x => AddQuestion(x));
        }
        public void DeleteChecklist()
        {
            Visible = false;
        }
        public void DeleteQuestion(List<ChecklistsQuestions> questions)
        {
            _ChecklistsQuestions.RemoveAll(x => !questions.Contains(x));
        }
    }
}