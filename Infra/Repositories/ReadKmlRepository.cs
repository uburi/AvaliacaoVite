using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;

namespace Infra.Repositories
{
    public class ReadKmlRepository : IReadKmlRepository
    {
        private readonly IMemoryCache _memoryCache;

        public ReadKmlRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<List<Placemark>> GetPlacemarkDataAsync(string kmlFilePath)
        {
            if (!_memoryCache.TryGetValue("placemarks", out List<Placemark> placemarks))
            {
                placemarks = LoadPlacemarkKmlFile(kmlFilePath);
                _memoryCache.Set("placemarks", placemarks);
            }

            return placemarks;
        }

        private List<Placemark> LoadPlacemarkKmlFile(string filePath)
        {
            var placemarks = new List<Placemark>();
            XNamespace ns = "http://www.opengis.net/kml/2.2";
            XDocument kmlDoc = XDocument.Load(filePath);
            var placemarkElements = kmlDoc.Descendants(ns + "Placemark");

            foreach (var placemarkElement in placemarkElements)
            {
                var placemark = new Placemark
                {
                    Cliente = placemarkElement.Element(ns + "ExtendedData")?
                                     .Elements(ns + "Data")
                                     .FirstOrDefault(e => e.Attribute("name")?.Value == "CLIENTE")?
                                     .Value ?? string.Empty,
                    Situacao = placemarkElement.Element(ns + "ExtendedData")?
                                       .Elements(ns + "Data")
                                       .FirstOrDefault(e => e.Attribute("name")?.Value == "SITUAÇÃO")?
                                       .Value ?? string.Empty,
                    Bairro = placemarkElement.Element(ns + "ExtendedData")?
                                                    .Elements(ns + "Data")
                                                    .FirstOrDefault(e => e.Attribute("name")?.Value == "BAIRRO")?
                                                    .Value ?? string.Empty,
                    RuaCruzamento = placemarkElement.Element(ns + "ExtendedData")?
                                                    .Elements(ns + "Data")
                                                    .FirstOrDefault(e => e.Attribute("name")?.Value == "RUA/CRUZAMENTO")?
                                                    .Value ?? string.Empty,
                    Referencia = placemarkElement.Element(ns + "ExtendedData")?
                                                    .Elements(ns + "Data")
                                                    .FirstOrDefault(e => e.Attribute("name")?.Value == "REFERENCIA")?
                                                    .Value ?? string.Empty,
                    Data = DateTime.TryParse(placemarkElement.Element(ns + "ExtendedData")?
                                                  .Elements(ns + "Data")
                                                  .FirstOrDefault(e => e.Attribute("name")?.Value == "DATA")?
                                                  .Value, out var date) ? date : DateTime.MinValue,
                    Coordenadas = placemarkElement.Element(ns + "ExtendedData")?
                                                    .Elements(ns + "Data")
                                                    .FirstOrDefault(e => e.Attribute("name")?.Value == "COORDENADAS")?
                                                    .Value ?? string.Empty,
                    GxMediaLinks = placemarkElement.Element(ns + "ExtendedData")?
                                                      .Elements(ns + "Data")
                                                      .FirstOrDefault(e => e.Attribute("name")?.Value == "gx_media_links")?
                                                      .Value ?? string.Empty
                };

                placemarks.Add(placemark);
            }

            return placemarks;
        }
    }
}

