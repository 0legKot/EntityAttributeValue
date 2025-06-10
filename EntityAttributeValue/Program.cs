using Bogus;
using EntityAttributeValue.Components;
using EntityAttributeValue.Components.EAV;
using Microsoft.EntityFrameworkCore;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EavDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<EavDbContext>();
    db.Database.Migrate();

    if (!db.Entities.Any()) {
        var faker = new Faker();
        const int entityCount = 1000;
        const int attrStringCount = 500;
        const int attrIntCount = 500;
        var rnd = new Random(123);

        var entities = Enumerable.Range(1, entityCount)
            .Select(i => new Entity { Name = faker.Company.CompanyName() })
            .ToList();

        var categories = faker.Commerce.Categories(attrStringCount + attrIntCount);

        var attrsString = categories.Take(attrStringCount)
            .Select(x => new AttributeDefinitionString { Name = x })
            .ToList();

        var attrsInt = categories.Skip(attrStringCount)
            .Select(x => new AttributeDefinitionInt { Name = x })
            .ToList();

        db.Entities.AddRange(entities);
        db.AttributesString.AddRange(attrsString);
        db.AttributesInt.AddRange(attrsInt);
        db.SaveChanges();

        var batchString = new List<EntityAttributeValueString>(5000);
        foreach (var e in entities) {
            foreach (var a in attrsString) {
                if (rnd.Next(10) != 2) continue;
                batchString.Add(new() {
                    EntityId = e.Id,
                    AttributeId = a.Id,
                    Value = faker.Commerce.ProductAdjective()
                });

                if (batchString.Count == 5000) {
                    db.EntityAttributeValuesString.AddRange(batchString);
                    db.SaveChanges();
                    batchString.Clear();
                }
            }
        }
        if (batchString.Count > 0) {
            db.EntityAttributeValuesString.AddRange(batchString);
            db.SaveChanges();
        }

        var batchInt = new List<EntityAttributeValueInt>(5000);
        foreach (var e in entities) {
            foreach (var a in attrsInt) {
                if (rnd.Next(10) != 2) continue;
                batchInt.Add(new() {
                    EntityId = e.Id,
                    AttributeId = a.Id,
                    Value = rnd.Next(0, 1000)
                });

                if (batchInt.Count == 5000) {
                    db.EntityAttributeValuesInt.AddRange(batchInt);
                    db.SaveChanges();
                    batchInt.Clear();
                }
            }
        }
        if (batchInt.Count > 0) {
            db.EntityAttributeValuesInt.AddRange(batchInt);
            db.SaveChanges();
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
