const URL_BUSCAR_PRODUTO = "/Produtos/Produto/";
const URL_GERAR_VENDA = "/Produtos/GerarVenda";
let produto;
let compra = [];
let __totalVenda__ = 0.0;

$("#posvenda").hide();

atualizarTotal();

function atualizarTotal() {
    $("#totalVenda").html(__totalVenda__);
}

function preencherFormulario() {
    $("#campoNome").val(produto.nome);
    $("#campoCategoria").val(produto.categoria.nome);
    $("#campoFornecedor").val(produto.fornecedor.nome);
    $("#campoPreco").val(produto.precoDeVenda);
}

function zerarFormulario() {
    $("#campoNome").val("");
    $("#campoCategoria").val("");
    $("#campoFornecedor").val("");
    $("#campoPreco").val("");
    $("#campoQuantidade").val("");
    $("#codProduto").val("");
}

function adicionarNaTabela(qtd) {
    if (produto != undefined) {
        let produtoTemp = {};
        Object.assign(produtoTemp, produto);
        let venda = { produto: produtoTemp, quantidade: qtd, subtotal: produtoTemp.precoDeVenda * qtd };
        __totalVenda__ += venda.subtotal;
        compra.push(venda);
        atualizarTotal();

        $("#compras").append(`
        <tr>
            <td>${produto.id}</td>
            <td>${produto.nome}</td>
            <td>${qtd}</td>
            <td>${produto.precoDeVenda} R$</td>
            <td>${produto.medicao}</td>
            <td>${produto.precoDeVenda * qtd} R$</td>
            <td><button class="btn btn-danger">Remover</button></td>
        <tr>
        `);
    }
}

$("#produtoForm").on("submit", function (event) {
    event.preventDefault();
    let qtd = parseInt($("#campoQuantidade").val());

    adicionarNaTabela(qtd);

    zerarFormulario();
    produto = undefined;
});

$("#pesquisar").click(async function () {
    let codProduto = $("#codProduto").val();

    if (codProduto == "" || codProduto == undefined) {
        alert("Informe o código do produto!");
        return;
    }

    $("#textPesquisar").html("Buscando...");
    $("#pesquisar").prop("disabled", true);

    await $.post(`${URL_BUSCAR_PRODUTO}${codProduto}`, function (dados, status) {

        produto = dados;

        switch (produto.medicao) {
            case 0:
                produto.medicao = "L"
                break;
            case 1:
                produto.medicao = "K"
            case 2:
                produto.medicao = "U"
                break;
            default:
                produto.medicao = "U"
                break;
        }

        preencherFormulario();
    }).fail(function () {
        alert("Produto inválido!");
    });

    $("#textPesquisar").html("Pesquisar");
    $("#pesquisar").prop("disabled", false);
});

$("#finalizarVendaBtn").click(function () {
    if (__totalVenda__ <= 0) {
        alert("Compra inválida, nenhum produto adicionado!");
        return;
    }

    let valorPago = $("#valorPago").val();

    if (!isNaN(valorPago)) {
        if (valorPago >= __totalVenda__) {
            $("#posvenda").show();
            $("#prevenda").hide();
            $("#valorPago").prop("disabled", true);

            let troco = valorPago - __totalVenda__;
            $("#troco").val(troco);

            compra.forEach(elemento => {
                elemento.produto = elemento.produto.id;
            });

            let venda = {
                total: __totalVenda__,
                troco: troco,
                produtos: compra
            };

            $.ajax({
                type: "POST",
                url: URL_GERAR_VENDA,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(venda),
                success: function (data) {
                    console.log(data);
                }
            });

        } else {
            alert("Valor pago é muito baixo!");
            return;
        }
    } else {
        alert("Valor pago, inválido!");
        return;
    }
});

$("#fecharModal").click(function () {
    $("#posvenda").hide();
    $("#prevenda").show();
    $("#valorPago").prop("disabled", false);
    $("#troco").val("");
    $("#valorPago").val("");
});
