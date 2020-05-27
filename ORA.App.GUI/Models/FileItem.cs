using ORA.API;
using ORA.App.GUI.ViewModels;

namespace ORA.App.GUI.Models
{
    public class FileItem
    {
        public File File { get; }

        private ClusterViewModel clusterViewModel;

        public FileItem(ClusterViewModel clusterViewModel, File file)
        {
            this.clusterViewModel = clusterViewModel;
            this.File = file;
        }
    }
}
