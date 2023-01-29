using CodeGenerator;

var generatorService = new CodeGeneratorService(1000);
var codes = new List<string>();


for (double i = 10000000000; i < 10000000000 + generatorService.TopSeed; i = i + generatorService.Offset)
{
    string code = generatorService.GenerateCode(i);
    double seed = generatorService.VerifyCode(code);

    if(i != seed)
    {
        throw new Exception("Could not verify the code");
    }

    Console.WriteLine(code);
    Console.WriteLine(seed);
    codes.Add(code);

}

Console.WriteLine($"Total unique number count : {codes.Distinct().Count()}");

Console.ReadLine();
