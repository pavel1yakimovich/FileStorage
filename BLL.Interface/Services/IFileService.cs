using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Method returns BllFile by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>BllFile</returns>
        BllFile GetFileEntity(int id);

        /// <summary>
        /// Method returns all files
        /// </summary>
        /// <returns>All files</returns>
        IEnumerable<BllFile> GetAllFileEntities();

        /// <summary>
        /// Method returns all public files
        /// </summary>
        /// <returns>Public files</returns>
        IEnumerable<BllFile> GetAllPublicFileEntities();

        /// <summary>
        /// Method returns all files of user
        /// </summary>
        /// <param name="user">Owner</param>
        /// <returns>Owner's files</returns>
        IEnumerable<BllFile> GetAllFileEntitiesOfUser(string user);

        /// <summary>
        /// Method returns all public files of user
        /// </summary>
        /// <param name="user">Owner</param>
        /// <returns>Owner's public files</returns>
        IEnumerable<BllFile> GetAllPublicFileEntitiesOfUser(string user);

        /// <summary>
        /// Creates file
        /// </summary>
        /// <param name="file">File</param>
        void CreateFile(BllFile file);

        /// <summary>
        /// Deletes file
        /// </summary>
        /// <param name="file">file</param>
        void DeleteFile(BllFile file);

        /// <summary>
        /// Updates file
        /// </summary>
        /// <param name="file">file</param>
        void UpdateFile(BllFile file);
    }
}
