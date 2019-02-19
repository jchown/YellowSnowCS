using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowSnow
{
    abstract public class Annotater
    {
        abstract public string GetWorkspaceRoot();

        abstract public bool IsInWorkspace(string filename);

        abstract public Annotations GetAnnotations(string filename);

        protected string workspaceRoot;
    }
}
