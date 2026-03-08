# Dawal [![.NET](https://github.com/suhdev/dawal/actions/workflows/dotnet.yml/badge.svg)](https://github.com/suhdev/dawal/actions/workflows/dotnet.yml)

A simple extensible expression language for .net applications.

## Syntax

Dawal has a simple syntax: everything is either a function call, a primitive value, or a variable reference.

### Primitive values

Dawal supports the following literal types:

| Type    | Example          |
|---------|------------------|
| Number  | `42`, `3.14`     |
| String  | `'hello'`, `"hi"`|
| Boolean | `true`, `false`  |
| Null    | `null`           |

### Comments

Comments are wrapped in `#` signs and can appear anywhere a value is expected:

```
EqualTo(100, 1000 #this is a comment#)
```

### Function calls

Functions are called with `FunctionName(arg1, arg2, ...)`. Arguments can be any expression (literals, variables, or nested function calls):

```
EqualTo(10, 10)
Not(EqualTo(10, null))
And(EqualTo(10, 10), LessThan(100, 1000))
```

### Variables

Variables are referenced with a `$` prefix and resolved from the evaluation context at runtime:

```
$myVar
$count
```

### Nested value access (member access)

Properties of variables can be accessed using dot notation. Chains of arbitrary depth are supported:

```
$user.name
$order.address.city
$product.details.dimensions.width
```

Variables and member access can be used anywhere a value is accepted — including as function arguments:

```
EqualTo($user.role, 'admin')
GreaterThan($order.total, 100)
And(EqualTo($user.active, true), GreaterThan($account.balance, 0))
```

## Usage

```c#
var compiler = new DawalCompiler();
var expression = compiler.Compile("EqualTo(10, 10)");

var result = await expression.First().EvaluateAsync<bool>(new BaseEvaluationContext(new IEvaluationFunction[]
{
  new EqualToFunction(),
}));

Assert.True(result);
```

### Using variables

Pass a dictionary of variables as the second argument to `BaseEvaluationContext`:

```c#
var compiler = new DawalCompiler();
var expression = compiler.Compile("EqualTo($user.role, 'admin')");

var user = new { Role = "admin" };
var ctx = new BaseEvaluationContext(
  new IEvaluationFunction[] { new EqualToFunction() },
  new Dictionary<string, object> { ["user"] = user });

var result = await expression.First().EvaluateAsync<bool>(ctx);
// result == true
```
