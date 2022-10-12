namespace Petroineos.CodingChallenge.IntradayReport.Abstractions
{
    public class ReportOptions
    {
        private string? _folderPath;
        public string FolderPath
        {
            get => string.IsNullOrEmpty(_folderPath) ? Directory.GetCurrentDirectory() : _folderPath;
            set => _folderPath = value;
        }
    }
}
