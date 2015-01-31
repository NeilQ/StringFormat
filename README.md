# StringFormat

Apply couple of format methods that similiar to the String.Format() method. 
But will not throw Exception when the placeholders's count in format string does not match the args, 
instead it will format the string dynamicly.  

```c#
StringFormat.Format("{0},{1},{2}", "a", "b", "c") --> "a,b,c"
```
```c#
StringFormat.Format("{0},{1},{2}", "a", "b") --> "a,b,null"
```
```c#
StringFormat.Format("{0},{1},{2}", "a", "b", "c", "d") --> "a,b,c"
```
```c#
StringFormat.Format("{{0}},{1},{2}", "a", "b", "c") --> "{0},b,c"
```
