using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagFiles.Explorer.Database;
using TagFiles.Explorer.Models;

namespace TagFiles.Tests.Explorer.Models;

[TestClass]
public class TagsManagerTest
{
    [TestInitialize]
    public async Task TestInitialize()
    {
        _connection = new("Filename=:memory:");
        _connection.Open();
        DbContextOptions<AppDbContext> dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;
        _dbContext = new AppDbContext(dbOptions);
        await _dbContext.Database.EnsureCreatedAsync();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Dispose();
        _connection.Dispose();
    }

    [TestMethod]
    public async Task GetAllTagsIsCorrect()
    {
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file1", new List<string> { "tag1", "tag2" }));
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file2", new List<string> { "tag3" }));
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file3", new List<string> { "tag1", "tag3" }));
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file4", new List<string> { "tag1", "tag2", "tag3" }));
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file5", new List<string> { "test" }));
        await _dbContext.SaveChangesAsync();

        TagsManager tagsManager = new(_dbContext);
        IReadOnlyList<Tag> tags = tagsManager.GetAllTags();

        Assert.AreEqual(4, tags.Count);
        Assert.AreEqual(3, tags.Single(x => x.Name == "tag1").Count);
        Assert.AreEqual(2, tags.Single(x => x.Name == "tag2").Count);
        Assert.AreEqual(3, tags.Single(x => x.Name == "tag3").Count);
        Assert.AreEqual(1, tags.Single(x => x.Name == "test").Count);
    }

    [TestMethod]
    public async Task SearchIsCorrect()
    {
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file1", new List<string> { "tag1", "tag2" }));
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file2", new List<string> { "tag3" }));
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file3", new List<string> { "tag1", "tag3" }));
        await _dbContext.FilesMetadata.AddAsync(new FileMetadata("file4", new List<string> { "tag1", "tag2", "tag3" }));
        await _dbContext.SaveChangesAsync();

        TagsManager tagsManager = new(_dbContext);
        IReadOnlyList<FileMetadata> searchResultTag1 = tagsManager.Search("tag1");
        IReadOnlyList<FileMetadata> searchResultTag2 = tagsManager.Search("tag2");
        IReadOnlyList<FileMetadata> searchResultTag3 = tagsManager.Search("tag3");

        Assert.AreEqual(3, searchResultTag1.Count);
        Assert.IsTrue(searchResultTag1.Any(x => x.Path == "file1"));
        Assert.IsTrue(searchResultTag1.Any(x => x.Path == "file3"));
        Assert.IsTrue(searchResultTag1.Any(x => x.Path == "file4"));
        Assert.AreEqual(2, searchResultTag2.Count);
        Assert.IsTrue(searchResultTag2.Any(x => x.Path == "file1"));
        Assert.IsTrue(searchResultTag2.Any(x => x.Path == "file4"));
        Assert.AreEqual(3, searchResultTag3.Count);
        Assert.IsTrue(searchResultTag3.Any(x => x.Path == "file2"));
        Assert.IsTrue(searchResultTag3.Any(x => x.Path == "file3"));
        Assert.IsTrue(searchResultTag3.Any(x => x.Path == "file4"));
    }

    [TestMethod]
    public async Task WorkWithManyFilesIsCorrect()
    {
        for (int i = 0; i < 10000; ++i)
        {
            await _dbContext.FilesMetadata.AddAsync(new FileMetadata(
                $"/path1/path2/path3/file{i}",
                new List<string> { i.ToString(), "tag-x" }));
        }

        await _dbContext.SaveChangesAsync();

        TagsManager tagsManager = new(_dbContext);
        IReadOnlyList<Tag> tags = tagsManager.GetAllTags();

        Assert.AreEqual(10001, tags.Count);
        Assert.AreEqual(10000, tags.Single(x => x.Name == "tag-x").Count);
    }

    private AppDbContext _dbContext = default!;
    private SqliteConnection _connection = default!;
}