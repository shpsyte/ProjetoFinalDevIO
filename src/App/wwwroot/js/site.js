﻿function AjaxModal() {
    $(document).ready(function() {
        $(function() {
            //$.ajaxSetup({ cache: false });
            $("a[data-modal]").on("click", function e() {
                $("#myModalContent").load(this.href, function() {
                    $("#myModal").modal(
                        {
                            keyboard: true
                        },
                        "show"
                    );
                    bindThisForm(this);
                });
                return false;
            });
        });

        // quando envia o novo endereco
        function bindThisForm(dialog) {
            $("form", dialog).submit(function() {
                $.ajax({
                    type: "POST",
                    url: this.action,
                    type: this.method,

                    data: $(this).serialize(),

                    success: function(data) {
                        if (data.sucess) {
                            $("#myModal").modal("hide");
                            $("#EnderecoTarget").load(data.url);
                        } else {
                            $("myModalContent").html(data);
                            bindThisForm(dialog);
                        }
                    }
                });

                return false;
            });
        }
    });
}

function AddCep() {
    let cep = document.querySelector("#Endereco_PostCode");

    cep.addEventListener("blur", function() {
        let valor = this.value;
        pesquisacep(valor);
    });
}

function limpa_formulario_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById("Endereco_Street").value = "";
    // document.getElementById("District").value = "";
    // document.getElementById("CityName").value = "";
    // document.getElementById("StateProvinceName").value = "";
    // document.getElementById("CityCode").value = "";
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.

        document.getElementById("Endereco_Street").value = conteudo.logradouro;
        // document.getElementById("District").value = conteudo.bairro;
        // document.getElementById("CityName").value = conteudo.localidade;
        // document.getElementById("StateProvinceName").value = conteudo.uf;
        // document.getElementById("CityCode").value = conteudo.ibge;
    } //end if.
    else {
        //CEP não Encontrado.
        limpa_formulario_cep();
        alert("CEP não encontrado.");
    }
}

function pesquisacep(valor) {
    //Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, "");

    //Verifica se campo cep possui valor informado.
    if (cep != "") {
        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {
            //Preenche os campos com "..." enquanto consulta webservice.
            document.getElementById("Endereco_Street").value = "...";
            // document.getElementById("District").value = "...";
            // document.getElementById("CityName").value = "...";
            // document.getElementById("StateProvinceName").value = "...";
            // document.getElementById("CityCode").value = "...";

            //Cria um elemento javascript.
            var script = document.createElement("script");

            //Sincroniza com o callback.
            script.src =
                "https://viacep.com.br/ws/" +
                cep +
                "/json/?callback=meu_callback";

            //Insere script no documento e carrega o conteúdo.
            document.body.appendChild(script);
        } //end if.
        else {
            //cep é inválido.
            limpa_formulario_cep();
            alert("Formato de CEP inválido.");
        }
    } //end if.
    else {
        //cep sem valor, limpa formulário.
        limpa_formulario_cep();
    }
}
