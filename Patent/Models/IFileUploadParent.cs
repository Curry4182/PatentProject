
namespace Patent.Models
{
    public interface IFileUploadParent
    {
        public void AddChildFileUpload(IFileUpload childUploadComponent);
    }
}
