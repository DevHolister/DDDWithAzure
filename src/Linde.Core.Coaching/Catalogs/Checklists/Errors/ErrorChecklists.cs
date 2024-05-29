using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linde.Core.Coaching.Catalogs.Checklists.Errors
{
    public static class ErrorChecklists
    {
        public static Error DuplicatedChecklist => Error.Conflict(
       code: "Checklist.Duplicated",
       description: "La lista de cotejo a guardar ya existe en la base de datos.");

        public static Error InvalidQuestion => Error.Conflict(
       code: "User.InvalidQuestion",
       description: "Se encontró una pregunta no válida.");
    }
}
