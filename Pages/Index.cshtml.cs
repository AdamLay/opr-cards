using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnePageRulesCards.Data;

namespace OnePageRulesCards.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
      _logger = logger;
    }

    //private const string TEMP_PATH = 
    private const string TEMP_PATH =
      @"C:\Users\adaml\Downloads\GFF_-_Orcs_Maraudeurs_beginner.rosz";
      //@"C:\temp\AH.ros";

    public Roster Roster { get; set; }

    public async Task OnGet()
    {
      var xml = new XmlSerializer(typeof(Roster));

      if (TEMP_PATH.EndsWith("rosz"))
      {
        await using (FileStream file = System.IO.File.OpenRead(TEMP_PATH))
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
        await using FileStream file = System.IO.File.OpenRead(TEMP_PATH);
        Roster = (Roster)xml.Deserialize(file);
      }

      var json = JsonConvert.SerializeObject(Roster);

      //await using Stream reader = new FileStream(TEMP_PATH, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

      //// Call the Deserialize method to restore the object's state.
      //var roster = (Roster)xml.Deserialize(reader);

      //Roster = roster;
    }
  }
}
