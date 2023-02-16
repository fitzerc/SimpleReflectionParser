using System.Diagnostics;
using System.Text.Json;
using FluentAssertions;

namespace ReflectionParser.Tests;

public class ParserTests
{
    [Fact]
    public void Parse_Line_Test()
    {
        //note lawn mower (index 4) is not in target object nor is it mapped so it is ignored
        var testLine = "mow lawn,100,mow the lawn,yard work,lawn mower,home maintenance";
        var mappingDictionary = JsonSerializer.Deserialize<Dictionary<int, string>>(GetTestMappingJson());

        var obj = new TodoItem();

        //Loop over array of strings (lines) and parse each into your desired object
        Debug.Assert(mappingDictionary != null, nameof(mappingDictionary) + " != null");
        var targetObject =
            Parser<TodoItem>.ParseLine(testLine, mappingDictionary, obj);

        targetObject.HasError.Should().Be(false);
        targetObject.TargetObject?.Name.Should().Be("mow lawn");
        targetObject.TargetObject?.Priority.Should().Be(100);
        targetObject.TargetObject?.Description.Should().Be("mow the lawn");
        targetObject.TargetObject?.Category.Should().Be("yard work");
        targetObject.TargetObject?.Type.Should().Be("home maintenance");
    }

    private string GetTestMappingJson()
    {
        var testMappingJson = "{" + Environment.NewLine;
        testMappingJson += "\"0\": \"Name\"," + Environment.NewLine;
        testMappingJson += "\"1\": \"Priority\"," + Environment.NewLine;
        testMappingJson += "\"2\": \"Description\"," + Environment.NewLine;
        testMappingJson += "\"3\": \"Category\"," + Environment.NewLine;
        testMappingJson += "\"5\": \"Type\"" + Environment.NewLine;
        testMappingJson += "}";

        return testMappingJson;
    }
}

//Sample Data Object for Testing
public class TodoItem
{
    public string? Name { get; set; }
    public int? Priority { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Type { get; set; }
}