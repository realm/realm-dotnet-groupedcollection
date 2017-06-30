# GroupedRealmCollection

A set of extension methods that can project [Realm Xamarin](https://realm.io/docs/xamarin/latest/) collections
to data bindable grouped collections that can be passed directly to a [Xamarin.Forms](https://www.xamarin.com/forms) ListView.

## Getting started

Just add the [Realm.GroupedCollection](https://www.nuget.org/packages/Realm.GroupedCollection) package to your project.

## Sample usage

```csharp
// In your ViewModel
Students = realm.All<School>().ToGroupedCollection(school => school.Students);
```

This can then be passed to your ListView:

```xml
<ListView ItemsSource="{Binding Students}" IsGroupingEnabled="true" GroupDisplayBinding="{Binding Key.Name}">
    <ListView.ItemTemplate>
        <DataTemplate>
            <TextCell Text="{Binding Name}"/>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

And your models would look something like:

```csharp
class Student : RealmObject
{
    public string Name { get; set; }
    
    public School School { get; set; }
}

class School : RealmObject
{
    public string Name { get; set; }
    
    [Backlink(nameof(Student.School))]
    public IQueryable<Student> Students { get; }
}
```

## Features

Both the returned collection and the inner collections are observable, so any changes will update the UI.

### Queries

```csharp
// Return schools ordered by Name, containing Students ordered by Age
var students = realm.All<School>().OrderBy(school => school.Name)
                    .ToGroupedCollection(school => school.Students.OrderBy(student => student.Age));
```

Supported query operations are limited by [Realm's query support](https://realm.io/docs/xamarin/1.5.0/api/linqsupport.html).

### [To-many relationships](https://realm.io/docs/xamarin/latest/#to-many-relationships)

```csharp
public class Dog : RealmObject
{
    public string Name { get; set; }
}

public class Person : RealmObject 
{
    public IList<Dog> Dogs { get; } 
}

var dogs = realm.All<Person>().ToGroupedCollection(person => person.Dogs);
```

Note that queries over `IList` properties are not supported and will result in a runtime exception. This is the
case because those queries are executed by LINQ and not by Realm itself.

## Using Backlinks to invert the group direction

Usually, a .NET grouping would go from the specific to the general:

```csharp
IEnumerable<Student> students = ...;
var groupedStudents = students.GroupBy(s => s.School);
```

Since Realm doesn't yet support grouping, we can achieve the same result by using [backlinks (inverse relationships)](https://realm.io/docs/xamarin/latest/#inverse-relationships).

Then, the grouping goes from the general to the specific:
```csharp
IQueryable<School> schools = realm.All<School>();
var groupedStudents = schools.ToGroupedCollection(s => s.Students);
```

## License

This project is released under the [MIT license](https://github.com/realm/realm-dotnet-groupedcollection/blob/master/LICENSE).
