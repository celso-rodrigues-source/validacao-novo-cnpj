using validacao_novo_cnpj;

class Program
{
    static void Main(string[] args)
    {
        List<string> listaCNPJ = new List<string>();

        listaCNPJ.Add("13.638.767/0001-92");
        listaCNPJ.Add("33.EMA.SAM/E007-81");
        listaCNPJ.Add("04.KA2.B01/A001-49");
        listaCNPJ.Add("a1.b2c.3d40/1a2b-41");

        foreach (var item in listaCNPJ)
        {
            Console.WriteLine($"O CNPJ {item} é {(Validacao.IsCnpjValido(item) ? "válido" : "inválido")}");
        }            

        Console.ReadKey();
    }
}