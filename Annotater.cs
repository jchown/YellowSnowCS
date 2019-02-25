using System.IO;

namespace YellowSnow
{
    abstract public class Annotater
    {
        public Annotations GetAnnotations(string filename)
        {
            if (File.Exists(filename))
                return GetAnnotationsFile(filename);

            if (Directory.Exists(filename))
                return GetAnnotationsDir(filename);

            throw new FileNotFoundException(filename);
        }

        abstract public string GetWorkspaceRoot();

        abstract public bool IsInWorkspace(string filename);

        abstract public Annotations GetAnnotationsFile(string filename);

        abstract public Annotations GetAnnotationsDir(string filename);

        protected string workspaceRoot;
    }
}
