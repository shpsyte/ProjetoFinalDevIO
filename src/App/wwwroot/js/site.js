function AjaxModal() {
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
