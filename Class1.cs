using Antlr4.Runtime;
using MyAntlr;
using MyAntlr.Content;

var fileName = "Content\\test.red"; // args[0] ?

var fileContents = File.ReadAllText(fileName);

var inputStream = new AntlrInputStream(fileContents);
 //hhhh
var simpleLexar = new SimpleLexer(inputStream);

var commonTokenStream = new CommonTokenStream(simpleLexar);

var simpleParser = new SimpleParser(commonTokenStream);

var simpleContext = simpleParser.program();

var visitor = new SimpleVisitor();

visitor.Visit(simpleContext);