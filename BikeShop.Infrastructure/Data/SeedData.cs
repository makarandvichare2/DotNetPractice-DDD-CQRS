using BikeShop.Core.Bikes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;

namespace BikeShop.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        if (await dbContext.Bikes.AnyAsync()) return; 

        await PopulateLookUpDataAsync(dbContext);
        await PopulateBikeDataAsync(dbContext);
    }

    public static async Task PopulateLookUpDataAsync(AppDbContext dbContext)
    {
        dbContext.BikeModels.AddRange(
            new Core.BikeModels.BikeModel("Zaka"),
            new Core.BikeModels.BikeModel("2021"),
            new Core.BikeModels.BikeModel("MHT 8.6"),
            new Core.BikeModels.BikeModel("Somerby"),
            new Core.BikeModels.BikeModel("Vengeance E"),
            new Core.BikeModels.BikeModel("Karkinos"),
            new Core.BikeModels.BikeModel("ATB 1"),
            new Core.BikeModels.BikeModel("HYB 8.8"),
            new Core.BikeModels.BikeModel("Somerby Electric Bike"),
            new Core.BikeModels.BikeModel("Shockwave DBS Superleggera"),
            new Core.BikeModels.BikeModel("Virtuoso"),
            new Core.BikeModels.BikeModel("SLR 8.9")
            );

        dbContext.Categories.AddRange(
            new Core.Categories.Category("Hybrid Electric Bike"),
            new Core.Categories.Category("Road Bike"),
            new Core.Categories.Category("Mountain Bike"),
            new Core.Categories.Category("BMX Bike")
            );

        dbContext.Manufacturers.AddRange(
            new Core.Manufacturers.Manufacturer("Carrera"),
            new Core.Manufacturers.Manufacturer("Boardman"),
            new Core.Manufacturers.Manufacturer("Indi"),
            new Core.Manufacturers.Manufacturer("Pendleton"),
            new Core.Manufacturers.Manufacturer("X-Rated"),
            new Core.Manufacturers.Manufacturer("Voodoo"),
            new Core.Manufacturers.Manufacturer("Assist")
            );
        await dbContext.SaveChangesAsync();
    }

    public static async Task PopulateBikeDataAsync(AppDbContext dbContext)
    {
        var filePath = Path.Combine(
            Directory.GetParent(Environment.CurrentDirectory).FullName,
            "BikeShop.Infrastructure",
            "SampleData",
            "bikes.json");
        using (StreamReader r = new StreamReader(filePath))
        {
            string json = r.ReadToEnd();
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (var item in array)
            {
                string manufacturerName = item.manufacturer;
                var manufacturer = dbContext.Manufacturers.FirstOrDefault(o => o.Name == manufacturerName);

                string modelName = item.model;
                var model = dbContext.BikeModels.FirstOrDefault(o => o.ModelName == modelName);

                string categoryName = item.category;
                var category = dbContext.Categories.FirstOrDefault(o => o.Name  == categoryName);

                string priceString = item.price;
                Price price = CreatePrice(priceString);

                string weightString = item.weight;
                Weight weight = CreateWeight(weightString);

                string colourString = item.colour;
                Colour colour = CreateColour(colourString);

                string imgurl = item.img_url;

                string refString = item.@ref;
                Guid @ref = Guid.Parse(refString);

                dbContext.Bikes.AddRange(new Bike(
                    manufacturer.Id,
                    model.Id, 
                    category.Id,
                    price,
                    weight, 
                    colour, 
                    imgurl, 
                    @ref));
            }
            await dbContext.SaveChangesAsync();
        }
    }

    private static Price CreatePrice(string price)
    {
        string currency = price.Substring(0, 1);
        decimal currencyValue = decimal.Parse(price.Substring(1), CultureInfo.InvariantCulture);
        return new Price(currency, currencyValue);
    }

    private static Weight CreateWeight(string weight)
    {
        string unit = weight.Substring(weight.Length -2);
        decimal weightValue = decimal.Parse(weight.Substring(0, weight.Length - 2), CultureInfo.InvariantCulture);
        return new Weight(unit, weightValue);
    }

    private static Colour CreateColour(string colour)
    {
        return new Colour(colour);
    }
}