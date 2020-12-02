using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DietaNoDietaApi.Model
{
    public class FatSecretAccessToken
    {
        public String item { get; set; }
        public String access_token { get; set; }
        public String expires_in { get; set; }

        public String token_type { get; set; }
        public String scope { get; set; }
    }
}
