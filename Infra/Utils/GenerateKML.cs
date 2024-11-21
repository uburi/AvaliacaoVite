using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infra.Utils
{
    public static class GenerateKML
    {
        public static byte[] GenerateKmlPlacemarks(List<Placemark> placemarks)
        {
            XNamespace kmlNamespace = "http://www.opengis.net/kml/2.2";

            XDocument kmlDoc = new XDocument(
                new XElement(kmlNamespace + "kml",
                    new XElement(kmlNamespace + "Document",
                        new XElement(kmlNamespace + "name", "DIRECIONADORES EM ARACAJU 1"),
                        new XElement(kmlNamespace + "description", "Exportação dos pontos de localização"),

                        from placemark in placemarks
                        select new XElement(kmlNamespace + "Placemark",
                            new XElement(kmlNamespace + "name", "Ponto " + placemark.Cliente),
                            new XElement(kmlNamespace + "description",
                                $"RUA/CRUZAMENTO: {placemark.RuaCruzamento}<br>REFERENCIA: {placemark.Referencia}<br>" +
                                $"BAIRRO: {placemark.Bairro}<br>SITUAÇÃO: {placemark.Situacao}<br>" +
                                $"CLIENTE: {placemark.Cliente}<br>DATA: {placemark.Data.ToShortDateString()}<br>" +
                                $"COORDENADAS: {placemark.Coordenadas}"),

                            new XElement(kmlNamespace + "ExtendedData",
                                new XElement(kmlNamespace + "Data", new XAttribute("name", "RUA/CRUZAMENTO"), placemark.RuaCruzamento),
                                new XElement(kmlNamespace + "Data", new XAttribute("name", "REFERENCIA"), placemark.Referencia),
                                new XElement(kmlNamespace + "Data", new XAttribute("name", "BAIRRO"), placemark.Bairro),
                                new XElement(kmlNamespace + "Data", new XAttribute("name", "SITUAÇÃO"), placemark.Situacao),
                                new XElement(kmlNamespace + "Data", new XAttribute("name", "CLIENTE"), placemark.Cliente),
                                new XElement(kmlNamespace + "Data", new XAttribute("name", "DATA"), placemark.Data.ToShortDateString()),
                                new XElement(kmlNamespace + "Data", new XAttribute("name", "COORDENADAS"), placemark.Coordenadas),
                                new XElement(kmlNamespace + "Data", new XAttribute("name", "gx_media_links"), placemark.GxMediaLinks)
                            ),

                            new XElement(kmlNamespace + "Point",
                                new XElement(kmlNamespace + "coordinates", placemark.Coordenadas)
                            )
                        )
                    )
                )
            );
            return Encoding.UTF8.GetBytes(kmlDoc.ToString());
        }
    }
}
