function fnPesquisarRepositorios() {
    $("#repositorios").fadeOut();

    setTimeout(function () {
        $(".c-loader").fadeIn();

    }, 500);

    $("#btnPesquisar").prop("disabled", true);
    $("#inputPesquisa").prop("disabled", true);

    var inputPesquisa = document.querySelector("#inputPesquisa").value;

    setTimeout(function () {
        $("#repositorios").load("/Home/PesquisarRepositorios", `pesquisa=${inputPesquisa}`, function () { fnPesquisarRepositorios_CallBack() });
    }, 1000);

}

function fnPesquisarRepositorios_CallBack() {
    $("#repositorios").fadeIn();
    $(".c-loader").hide();
    $("#btnPesquisar").prop("disabled", false);
    $("#inputPesquisa").prop("disabled", false);
    fnTratarDescricao();
}

function fnObterRepositorio(id, elementoImg) {
    $(".iconDetalhes").css("pointer-events", "none");
    $(".iconDetalhes").css("opacity", "0.3");
    var iconLoading = $(elementoImg).prev();
    $(elementoImg).hide();
    $(iconLoading).show();
    $("#modalRepositorio .modal-body").load("/Home/ObterRepositorio", `id=${id}`, function () { fnObterRepositorio_CallBack(elementoImg) });
}

function fnObterRepositorio_CallBack(elementoImg) {
    $(elementoImg).show();
    var iconLoading = $(elementoImg).prev();
    $(iconLoading).hide();
    $("#modalRepositorio").modal("show");
    $(".iconDetalhes").css("pointer-events", "auto");
    $(".iconDetalhes").css("opacity", "1");
}

function fnTratarDescricao() {
    $(".descricaoRepositorio").each(function (i) {
        var text = $(this).text();
        var len = text.length;
        if (len > 100) {
            var query = text.split(" ", 15);
            query.push('...');
            res = query.join(' ');
            $(this).text(res);
        }
    });

}

$("#inputPesquisa").on('keypress', function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
        $(".btnPesquisar").click();
    }
})

$("#inputPesquisaUsuario").on('keypress', function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
        $(".btnPesquisar").click();
    }
})



