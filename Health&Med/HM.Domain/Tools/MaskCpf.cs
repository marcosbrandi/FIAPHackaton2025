namespace HM.Domain
{
    public static class Tools
    {
        public static string MaskCpf(string cpf)
        {
            return string.Format("{0}.***.***-{1}",

                cpf.Substring(0, 3),

                cpf.Substring(9, 2));

        }
    }
}

