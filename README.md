# Dawal [![.NET](https://github.com/suhdev/dawal/actions/workflows/dotnet.yml/badge.svg)](https://github.com/suhdev/dawal/actions/workflows/dotnet.yml)

A simple extensible expression language for .net applications.

## Syntax

Dawal has very simple syntax, everything is either a function call or a primitive value. See examples below:

### Example 1

```
EqualTo(10, 10)
```

### Example 2

```
Not(EqualTo(10, null))
```

### Example 3

```
EqualTo(100, 1000 #this is a comment#)
```

## Usage

```c#
var scanner = new DawalCompiler();
var expression  = compiler.Compile(@"EqualTo(10, 10)");

var result = expression.First().EvaluateAsync<bool>(new BaseEvaluationContext(new IEvaluationFunction[]{
  new EqualToFunction(),
}));

Assert.Equals(result, true);
```
