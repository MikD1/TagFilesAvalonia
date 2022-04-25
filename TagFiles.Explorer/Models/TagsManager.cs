using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TagFiles.Explorer.Database;

namespace TagFiles.Explorer.Models;

public class TagsManager
{
    public TagsManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _filesMetadata = default!;
        _tags = default!;

        // TODO: Move to async method
        LoadFilesMetadata().GetAwaiter().GetResult();
    }

    public async Task AddFiles(string[] paths, string[] tags)
    {
        // Add files to database
        await LoadFilesMetadata();
    }

    public IReadOnlyList<Tag> GetAllTags()
    {
        return _tags.AsReadOnly();
    }

    public IReadOnlyList<FileMetadata> Search(string tagName)
    {
        return _filesMetadata
            .Where(x => x.Tags.Any(t => t == tagName))
            .ToList();
    }

    private async Task LoadFilesMetadata()
    {
        _filesMetadata = await _dbContext.FilesMetadata.ToListAsync();

        Dictionary<string, int> tags = new();
        foreach (string tagName in _filesMetadata.SelectMany(metadata => metadata.Tags))
        {
            if (tags.ContainsKey(tagName))
            {
                tags[tagName]++;
            }
            else
            {
                tags.Add(tagName, 1);
            }
        }

        _tags = tags.Select(x => new Tag(x.Key, x.Value)).ToList();
    }

    private readonly AppDbContext _dbContext;
    private List<FileMetadata> _filesMetadata;
    private List<Tag> _tags;
}