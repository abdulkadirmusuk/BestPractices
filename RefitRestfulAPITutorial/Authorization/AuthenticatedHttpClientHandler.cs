﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RefitRestfulAPITutorial.Authorization
{
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly Func<Task<string>> getToken;
        public AuthenticatedHttpClientHandler(Func<Task<string>> getToken)
        {
            if (getToken == null) throw new ArgumentNullException(nameof(getToken));
            this.getToken = getToken;
        }

        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                var token = await getToken().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
