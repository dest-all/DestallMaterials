namespace DestallMaterials.CodeGeneration.Environment
{
    public class ProjectLocation
    {
        public readonly string ProjectName;

        public ProjectLocation(string projectName, string location)
        {
            ProjectName = projectName;
            Location = location;
        }

        public readonly string Location;
    }
}
