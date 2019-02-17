# Result

This is an implementation of **Result** class

Result is exactly what it sounds like - a result from an operation. It can be either **Success** or **Error**.

## Examples

* Easily convert any value to Result without annoying extensions methods

```csharp
Result<int, string> GetValue(int key)
{
try
{
    var value = _repository.Find(key);
    return Result.Success(value);
}
catch (Exception ex)
{
    return Result.Fail(ex.message);
}
}
```

* Map/FlatMap result

```csharp
//use Map function
Result<int, Unit> result = ...
Result<bool, Unit> mappedResult = result.Map(v => v % 2 == 0);
//optionaly map error as well
Result<bool, string> mapperResult = result.Map(v => v % 2 == 0, _ => "error");

//or use LINQ
var result = from x in Double(10)
             from y in Double(x)
             from z in Double(y)
             select x + y + z;

Result<int, Unit> Double(int value)
{
    ...
}

```

* Match on Result

```csharp

Result<int, string> result = ...

IActionResult ret = result.Match(
    success: v => Ok(v),
    error: e => InternalServerError(e)
);
```
