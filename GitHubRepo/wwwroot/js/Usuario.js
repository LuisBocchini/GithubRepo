function fnPesquisarUsuarios() {
    $("#usuarios").fadeOut();

    setTimeout(function () {
        $(".c-loader").fadeIn();

    }, 500);

    $(".btnPesquisar").prop("disabled", true);
    $(".inputPesquisa").prop("disabled", true);

    var inputPesquisa = document.querySelector("#inputPesquisaUsuario").value;

    setTimeout(function () {
        $("#usuarios").load("/Usuario/PesquisarUsuarios", `pesquisa=${inputPesquisa}`, function () { fnPesquisarUsuarios_CallBack() });
    }, 1000);

}

function fnPesquisarUsuarios_CallBack() {
    $("#usuarios").fadeIn();
    $(".c-loader").hide();
    $(".btnPesquisar").prop("disabled", false);
    $(".inputPesquisa").prop("disabled", false);
}

function fnObterUsuario(id, elementoImg) {
    $(".iconDetalhes").css("pointer-events", "none");
    $(".iconDetalhes").css("opacity", "0.3");
    var iconLoading = $(elementoImg).prev();
    $(elementoImg).hide();
    $(iconLoading).show();

    $("#modalUsuario .modal-body").load("/Usuario/ObterUsuario", `id=${id}`, function () { fnObterUsuario_CallBack(elementoImg) });
}

function fnObterUsuario_CallBack(elementoImg) {
    $(elementoImg).show();
    var iconLoading = $(elementoImg).prev();
    $(iconLoading).hide();
    $("#modalUsuario").modal("show");
    $("#listaRepositorios").css("max-height", "300px");
    $("#listaRepositorios").css("height", "auto");
    $(".iconDetalhes").css("pointer-events", "auto");
    $(".iconDetalhes").css("opacity", "1");
}

