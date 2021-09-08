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
using Newtonsoft.Json;
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
      //var xml = new XmlSerializer(typeof(Roster));

      //if (TEMP_PATH.EndsWith("rosz"))
      //{
      //  await using (FileStream file = System.IO.File.OpenRead(TEMP_PATH))
      //  using (var zip = new ZipArchive(file, ZipArchiveMode.Read))
      //  {
      //    if (zip.Entries.Any())
      //    {
      //      await using Stream stream = zip.Entries.First().Open();
      //      Roster = (Roster)xml.Deserialize(stream);
      //    }
      //  }
      //}
      //else
      //{
      //  await using FileStream file = System.IO.File.OpenRead(TEMP_PATH);
      //  Roster = (Roster)xml.Deserialize(file);
      //}
      
    }

    public async Task OnPost()
    {
      var xml = new XmlSerializer(typeof(Roster));
      
      if (RosterFile.FileName.EndsWith("rosz"))
      {
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
      else
      {
        await using Stream file = RosterFile.OpenReadStream();
        Roster = (Roster)xml.Deserialize(file);
      }
    }
  }
}
