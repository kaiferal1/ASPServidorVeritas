/*
 *
 * login-register modal
 * Autor: Creative Tim
 * Web-autor: creative.tim
 * Web script: http://creative-tim.com
 * 
 */

function showLoginForm(){
    $('#loginModal .registerBox').fadeOut('fast',function(){
        $('.loginBox').fadeIn('fast');
        $('.register-footer').fadeOut('fast',function(){
            $('.login-footer').fadeIn('fast');    
        });
        
        $('.modal-title').html('Login with');
    });       
     $('.error').removeClass('alert alert-danger').html(''); 
}

function openLoginModal(){
    showLoginForm();
    setTimeout(function(){
        $('#loginModal').modal('show');
    }, 230);
}

function loginAjax() {

    var params = new Object();
    params.usuario = $("#email").val();
    params.contrasena = $("#password").val();
    params.bandera = 0;
    params = JSON.stringify(params);

    $.ajax({
        type: "POST",
        url: "../../ServidorMetodhs.aspx/Login",
        data: params,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data.d);
            if (data.d == 0) {
                shakeModal();
            } else if (data.d == 1) {
                window.location.replace("dashboard.html");
            }
        },
        failure: function (data) {
            console.log(data);
            alert("Existio un Problema intentelo mas tarde");
        }
    });
}

function shakeModal(){
    $('#loginModal .modal-dialog').addClass('shake');
             $('.error').addClass('alert alert-danger').html("Usuario o Contraseña no valido");
             $('input[type="password"]').val('');
             setTimeout( function(){ 
                $('#loginModal .modal-dialog').removeClass('shake'); 
    }, 1000 );
}