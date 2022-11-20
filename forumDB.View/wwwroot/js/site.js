// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var escondido = true;
$("#olhoSenha").addClass("bi-eye");
function mostrarSenha() {
    if (escondido) {
        $("#olhoSenha").removeClass("bi-eye");
        $("#olhoSenha").addClass("bi-eye-slash");
        $("#senha").attr("type", "text");
        $("#senha2").attr("type", "text");
        escondido = false;
    } else {
        $("#olhoSenha").removeClass("bi-eye-slash");
        $("#olhoSenha").addClass("bi-eye");
        $("#senha").attr("type", "password");
        $("#senha2").attr("type", "password");
        escondido = true;
    }
}

function compararSenhas() {
    if ($("#senha").val() != $("#senha2").val() && $("#senha2").val() != '') {
        $("#senha2").addClass("is-invalid");
    } else {
        $("#senha2").removeClass("is-invalid");
    }
}

function timeago() {
    const formatter = new Intl.RelativeTimeFormat('pt');

    const diff = new Date() - new Date('11/18/2022 18:41:11');

    //const x = formatter.format(-diff / (1000 * 60 * 60 * 24), 'days');
    const x = formatter.format(-diff / (1000 * 60 * 60), 'hours');

    console.log(x)
    console.log($("#tempoatras").attr("title"))

    $("[id=tempoatras]").val(x)
    console.log($("[id=tempoatras]").val())
    
}