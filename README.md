# HtmlBuilder

This is a fairly simple, but really clean, in my opinion, library for building simple html. You can build individual elements through classes like **HtmlBuilderElement** or you can format entire Html Documents through the **HtmlBuilderDocument** class. 

### A Little Background ###
I get it, there are plenty of libraries for working with HTML out there, plenty in .NET itself, however I looked through several and just didn't really like the way they worked and also didn't want to have all the extra baggage of methods and things I didn't need. What I really wanted was  
- Something simple
- Something I had complete control over
- Something that was built with a specific purpose in mind

See, I actually built this for something at work. I'm redoing our report interface to output HTML into a WebBrowser control in WPF, and I only need the simplest HTML that will always be used with one specific type of browser. So, this seemed like a really fun and useful project to work, and so far, I really like how it has turned out.

### Creating an Element ###
The constructor for the **HtmlBuilderElement** class has 3 versions including the overloads. They allow you to instantiate a new **HtmlBuilderElement** with

1. A *tag* (i.e. div, table, span, body, etc.)
2. A *tag* and an *id*
3. A *tag*, an *id*, and a *class*

So, say you want to create a *div* element without a class or id. Using C# (as all my examples will) you would do

```csharp
HtmlBuilderElement div = new HtmlBuilderElement("div"); 
```
However, if you wanted to add a class called "redBackground", you could do so through the **CssClass** property on your *div* object. 

```csharp
div.CssClass.AddClass("redBackground");
```

You can add as many as you want, and remove them with the very originally named **RemoveClass(string *className*)** method.

Now that we've got the object, how do we get the HTML from it? Simple, just called everyone's favorite **ToString()** method, and we get

```html
<div class='redBackground'></div>
```
Say you want that to have a child element, though, how would you do that? Easy just create another element to nest

```csharp
HtmlBuilderElement span = new HtmlBuilderElement("span");
```

Then add it to the list property **Children** of the parent element.

```csharp
div.Children.Add(span);
```

The best part about this is that if you want to output an element and all its children, you only have to call **ToString()** on the top-level element. So, if I call **div.ToString()** after we added that new element to it, it will look like this

```html
<div class='redBackground'><span></span></div>
```

### Attributes ###
Attributes are also really easy to work with if I do say so myself. Using the previous example, say you want to add an attribute, other than *class* or *id* which have their own classes, just use the **HtmlBuilderAttribute** class. Say you want to add an attribute to that span called *data-order-number* and give it a value of *3B2*, then all you have to do is call the **Add** method on the property list **Attributes** of your **HtmlBuilderElement**.

```csharp
span.Attributes.Add(new HtmlAttribute("data-order-number", "3B2'));
```

Now, when you call the **ToString()** method on the *div* variable, it will return this

```html
<div class='redBackground'><span data-order-number='3B2'></span></div>
```

Something to keep in mind is that empty attributes (i.e. data-order-number='') will not output in the string. It will be like they're not even in the **Attributes** list.

##### Removing Attributes #####
I wanted a little disclaimer here. While it may seem counterintuitive, you don't remove attributes simply by calling *Remove()* on the **Attributes** property. You actually call **RemoveAttribute(string *attributeName*)** on the **HtmlBuilderElement** itself. So, if you want to remove the attribute we added, you would do so like this

```csharp
span.RemoveAttribute("data-order-number");
```
This was a design choice **AND** limitation. You can't override the *Remove()* method for the *List<T>* class, you have to implement *IList<T>* in a new class, then do it, or inherit *List<T>* in a subclass and write your own method, and both of those options seemed like unnecessary complexity. So, you can use the **Remove()** method in the *List<T>* class, but it will require a variable or use of *LINQ* or something to reference the proper object to be removed from the list, which is possible, but much more difficult then the route I chose. See, *I did it for you, world. I did it for you...*

### Adding Content ###
Say you want to add text to that *span* element from earlier, how do you do it? You simply add a new **HtmlBuilderContent** object to the **Children** property of *span*. See, **HtmlBuilderContent** is a subclass of **HtmlBuilderElement**, which makes outputting it and adding it to the flow easy. You simply pass what text you want as an argument to the constructor when you instantiate it, and you're done!

```csharp
span.Children.Add(new HtmlBuilderContent("Hello, Interwebz!"));
```

Now, when you output *div*, it will return

```html
<div class='redBackground'><span data-order-number='3B2'>Hello, Interwebz!</span></div>
```

### Creating an Html Document ###
Okay, so, now you want to create an entire HTML page. That means you'll have to add a *head* element and *body* and *title* and so-on, right? **HELL NO**. That would suck. Just create a new **HtmlBuilderDocument** object and it handles that crap for you! All you have to do is set the doctype you want through the **DocType** property (you'll have to format the whole string yourself though, so don't forget the opening and closing arrows, etc.), add the string for the *src* and and *href* attributes of the *script* and *style* tags through the properties **Stylesheets** and **Scripts**, and call our trusty **ToString()** method. Now, this will get you what you ***need***, but there are some static methods that may help you get what you ***want***.

##### Cleanup #####
Now, I'm not perfect, so when this outputs, there will be one annoying thing -- *lots of unnecessary whitespace*. This is a result of the way I parse and format the text, so the best way to handle this is to call the static method of the **HtmlBuilderDocument** class, **Cleanup(string *html*)** which returns a cleaned up version of your HTML string.

##### Pretty Print #####
When you output your **HtmlBuilderDocument** it will output as a single-line string. This won't affect how it renders in the browser or anything because it will parse and format it the best it can anyway, because it is smarter than us, and it knows it, however, if you decide that you want to see the HTML formatted in a tree-like markup structure, the way it *should* look, then call the static method **PrettyPrint(string *html*)** from the **HtmlBuilderDocument** class and it will return a version of the string with new lines and indentations to help it look totally formatted. Don't get me wrong, it has some drawbacks based on how it works internally, but it still *prints pretty*.

### So... ###
Through all that, plus everything we've already went over, you can output a completely valid and super sexy HTML string for whatever you need. Here's a little demo that is included in the Console Program called "ClassTest" in the Solution.

```csharp
HtmlBuilderDocument doc = new HtmlBuilderDocument("HtmlBuilder Rocks");

doc.DocType = "<!DOCTYPE html>";

doc.AddStylesheet(@"css/main.css");
doc.AddStylesheet(@"css/backgrounds.css");

doc.AddScript(@"js/jquery.js");

HtmlBuilderElement div = new HtmlBuilderElement("div", "wrapper");
HtmlBuilderElement p = new HtmlBuilderElement("p");
string baconFiller = @"Bacon ipsum dolor amet spare ribs beef ribs porchetta meatloaf ham shoulder ham hock bresaola ball tip rump kielbasa swine alcatra kevin. Turducken andouille jowl, corned beef short ribs beef beef ribs flank fatback pork belly shank frankfurter cupim shoulder. Sirloin meatloaf porchetta t-bone. Sirloin kevin venison meatball tenderloin flank turducken pig tongue t-bone cow corned beef alcatra. Kielbasa landjaeger ball tip prosciutto salami pork chop tail rump fatback.";
p.Children.Add(new HtmlBuilderContent(baconFiller));

doc.Body.Children.Add(div);
doc.Body.Children.Add(p);

string output = doc.ToString();
output = HtmlBuilderDocument.CleanupHtml(output);
output = HtmlBuilderDocument.PrettyPrint(output);

Console.WriteLine(output);

//This line just makes the console window stay open, it doesn't actually do anything
Console.In.Read();
```

Here is the output

```html
<!DOCTYPE html>
<html>
	<head>
		<title>HtmlBuilder Rocks
		</title>
		<link rel='stylesheet' href='css/main.css'>
		</link>
		<link rel='stylesheet' href='css/backgrounds.css'>
		</link>
	</head>
	<body>
		<div Id='wrapper'>
		</div>
		<p>Bacon ipsum dolor amet spare ribs beef ribs porchetta meatloaf ham shoulder ham hock bresaola ball tip rump kielbasa swine alcatra kevin. Turducken andouille jowl, corned beef short ribs beef beef ribs flank fatback pork belly shank frankfurter cupim shoulder. Sirloin meatloaf porchetta t-bone. Sirloin kevin venison meatball tenderloin flank turducken pig tongue t-bone cow corned beef alcatra. Kielbasa landjaeger ball tip prosciutto salami pork chop tail rump fatback.
		</p>
		<script src='js/jquery.js'>
		</script>
	</body>
</html>
```
Which doesn't look half-bad, in my honest opinion. Here it is if you hadn't called the **PrettyPrint** method.

```html
<!DOCTYPE html><html><head><title>HtmlBuilder Rocks</title><link rel='stylesheet' href='css/main.css'></link><link rel='stylesheet' href='css/backgrounds.css'></link></head><body><div Id='wrapper'></div><p>Bacon ipsum dolor amet spare ribs beef ribs porchetta meatloaf ham shoulder ham hock bresaola ball tip rump kielbasa swine alcatra kevin. Turducken andouille jowl, corned beef short ribs beef beef ribs flank fatback pork belly shank frankfurter cupim shoulder. Sirloin meatloaf porchetta t-bone. Sirloin kevin venison meatball tenderloin flank turducken pig tongue t-bone cow corned beef alcatra. Kielbasa landjaeger ball tip prosciutto salami pork chop tail rump fatback.</p><script src='js/jquery.js'></script></body></html>
```
