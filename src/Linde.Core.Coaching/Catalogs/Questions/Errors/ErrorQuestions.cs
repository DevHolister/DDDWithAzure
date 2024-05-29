using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Questions.Errors
{
    public static class ErrorQuestions
    {
        public static Error DuplicatedQuestion => Error.Conflict(
       code: "Question.Duplicated",
       description: "La opregunta a guardar ya existe en la base de datos.");

        public static Error InvalidActivity => Error.Conflict(
       code: "User.InvalidActivity",
       description: "Se encontró una actividad no válida.");
    }
}
