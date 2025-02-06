namespace HM.Domain
{
    public static class Tools
    {
        public static string MaskCpf(string cpf)
        {

            if (cpf.Length != 11)

                throw new ArgumentException("O CPF deve ter 11 dígitos.");

            // Mascarar os primeiros 6 dígitos do CPF

            return string.Format("***.***.{0}-{1}",

                cpf.Substring(6, 3),

                cpf.Substring(9, 2));

        }
    }
}

