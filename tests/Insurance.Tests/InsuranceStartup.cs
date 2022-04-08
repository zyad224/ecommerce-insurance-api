using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace Insurance.Tests
{
    public class InsuranceStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {                  
                    ep.MapGet(
                     "products",
                     context =>
                     {
                         var products = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Cowon Plenue D Gold",
                                                       productTypeId = 12,
                                                       salesPrice = 300

                                                   },
                                                   new
                                                   {
                                                       id = 2,
                                                       name = "Canon Powershot SX620 HS Black",
                                                       productTypeId = 33,
                                                       salesPrice = 195

                                                   },
                                                   new
                                                   {
                                                       id = 3,
                                                       name = "Cowon Plenue D Gold",
                                                       productTypeId = 12,
                                                       salesPrice = 750

                                                   },
                                                   new
                                                   {
                                                       id = 4,
                                                       name = "Cowon Plenue D Gold",
                                                       productTypeId = 12,
                                                       salesPrice = 2000

                                                   },
                                                   new
                                                   {
                                                       id = 5,
                                                       name = "Iphone 7",
                                                       productTypeId = 32,
                                                       salesPrice = 1000

                                                   },
                                                   new
                                                   {
                                                       id = 6,
                                                       name = "Dell laptop",
                                                       productTypeId = 21,
                                                       salesPrice = 3000

                                                   },
                                                   new
                                                   {
                                                       id = 7,
                                                       name = "Sony laptop",
                                                       productTypeId = 21,
                                                       salesPrice = 499

                                                   },
                                                   new
                                                   {
                                                       id = 8,
                                                       name = "Android phone",
                                                       productTypeId = 32,
                                                       salesPrice = 499

                                                   },
                                                   new
                                                   {
                                                       id = 9,
                                                       name = "HP laptop",
                                                       productTypeId = 21,
                                                       salesPrice = 499

                                                   },
                                                   new
                                                   {
                                                       id = 10,
                                                       name = "Canon Powershot SX620 HS Black",
                                                       productTypeId = 33,
                                                       salesPrice = 195

                                                   },
                                                   new
                                                   {
                                                       id = 11,
                                                       name = "SlR camera 1",
                                                       productTypeId = 35,
                                                       salesPrice = 3000

                                                   },
                                                    new
                                                   {
                                                       id = 12,
                                                       name = "Sony Mp3",
                                                       productTypeId = 15,
                                                       salesPrice = 3000

                                                   },
                                               };
                         return context.Response.WriteAsync(JsonConvert.SerializeObject(products));
                     }
                 );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 12,
                                                       name = "MP3 players",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 33,
                                                       name = "Digital cameras",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 32,
                                                       name = "Smartphones",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 21,
                                                       name = "Laptops",
                                                       canBeInsured = true
                                                   },
                                                    new
                                                   {
                                                       id = 35,
                                                       name = "SLR cameras",
                                                       canBeInsured = false
                                                   },
                                                    new
                                                   {
                                                       id = 15,
                                                       name = "MP3 players",
                                                       canBeInsured = true
                                                   },
                                                      new
                                                   {
                                                       id = 16,
                                                       name = "SLR cameras",
                                                       canBeInsured = true
                                                   }

                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }
}
