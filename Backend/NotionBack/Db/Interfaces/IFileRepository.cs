using System;
using File = NotionBack.Db.Models.fileStructure.File;

namespace NotionBack.Db.Interfaces;

public interface IFileRepository : IModelRepository<File> { }
