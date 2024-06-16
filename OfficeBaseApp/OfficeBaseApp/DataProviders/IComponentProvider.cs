using OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
namespace OfficeBaseApp.DataProviders;

public interface IComponentProvider
{
    //select
    List<Component> GetSpecificColumns();

    string AnonymousClass();

    List<string> GetUniqueComponentNames();

    float GetMinimumPriceOfAllComponents();

    // order by

    List<Component> OrderByName();

    List<Component> OrderByNameDescending();

    List<Component> OrderByNameAndPrice();

    List<Component> OrderByNameAndPriceDesc();

    //where

    List<Component> WhereStartsWith(string prefix);
    List<Component> WhereStartsWithAndPriceIsGreatherThan(string prefix, float price);
    List<Component> WhereDescriptionIs(string description);


    //first, last, single

    Component FirstByDescription(string description);
    Component? FirstOrDefaultByDescriptionWithDefault(string description);
    Component LastByDescription(string description);
    Component SingleById(int id);
    Component SingleOrDefualtById(int id);


    //take

    List<Component> TakeComponents(int howMany);
    List<Component> TakeCars(Range range);
    List<Component> TakeComponentsWhileNameStartsWith(string prefix);


    //skip

    List<Component> SkipComponents(int howMany);
    List<Component> SkipComponentsWhileNameStartsWith(string prefix);


    //distinct

    List<string> DistinctAllNames();
    List<Component> DistinctByNames();


    //chunk

    List<Component[]> ChunkComponents(int size);


















}
