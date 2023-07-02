using EQUTech.Core.Grpc.Models.JokerBoard;

namespace EQUTech.JokerBoard.Data.Models;

public class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = "";

    public string? Description { get; set; }

    public Category? Parent { get; set; }

    public CategoryItem ToCategoryItem()
    {
        var item = new CategoryItem
        {
            Id = Id,            
            Name = Name
        };

        if(Parent != null)
        {
            item.Pid = Parent.Id;
        }

        return item;
    }

    public static CategoryItems ToCategoryItemTree(List<Category> list)
    {
        var data = list
            .Select(item => item.ToCategoryItem())
            .ToDictionary(item => item.Id);

        var res = new CategoryItems();

        foreach (var item in list)
        {
            if (item.Parent != null)
            {
                var parent = data[item.Parent.Id];

                parent.Categories ??= new CategoryItems();

                parent.Categories.List.Add
                (
                    data[item.Id]
                );
            }
            else
            {
                res.List.Add
                (
                    data[item.Id]
                );
            }
        }

        return res;
    }
}
