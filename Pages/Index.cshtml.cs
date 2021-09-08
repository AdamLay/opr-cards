using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnePageRulesCards.Data;

namespace OnePageRulesCards.Pages
{
  [BindProperties]
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
      _logger = logger;
    }

    public Roster Roster { get; set; }
    public IFormFile RosterFile { get; set; }

    public async Task OnGet()
    {

    }

    public async Task OnPost()
    {
      try
      {
        // Assume it's a rosz file...
        await ParseRosz();
      }
      catch (Exception)
      {
        // ...if this fails, try and parse it as uncompressed ros file
        await ParseRos();
      }
    }

    private async Task ParseRosz()
    {
      var xml = new XmlSerializer(typeof(Roster));

      await using (Stream file = RosterFile.OpenReadStream())
      using (var zip = new ZipArchive(file, ZipArchiveMode.Read))
      {
        if (zip.Entries.Any())
        {
          await using Stream stream = zip.Entries.First().Open();
          Roster = (Roster)xml.Deserialize(stream);
        }
      }
    }

    private async Task ParseRos()
    {
      var xml = new XmlSerializer(typeof(Roster));

      await using Stream file = RosterFile.OpenReadStream();
      Roster = (Roster)xml.Deserialize(file);
    }
  }
}
