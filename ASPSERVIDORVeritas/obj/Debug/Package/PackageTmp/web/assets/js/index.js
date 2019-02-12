angular.module("mdlAppWeb", ['ngResource'])
    .controller("ctrlnav", function ($http, serv, $rootScope) {
        var scope = this;
        /******************************************************************/
        /*Inicio*/
        scope.iniClientes = "";
        scope.iniError = "";
        scope.iniFactTest = "";
        scope.iniCredTest = "";
        scope.iniFactInsp = "";
        scope.iniCredInsp = "";
        /*Clientes*/
        scope.todos = [];
        scope.bCliente = "";
        /*Servicios*/
        scope.errores = [];
        scope.bErrores = "";
        /*Plantillas*/
        scope.plantillas = [];
        /*Facturas*/
        scope.enviadas = [];
        scope.noenviadas = [];
        scope.bEnviadas = "";
        scope.bNoEnviadas = "";
        /******************************************************************/
        /*Controles jquery*/
        var jInicio = $("#inicio")
            , jCliente = $("#cliente")
            , jServicio = $("#servicio")
            , jUsuario = $("#usuarios")
            , jPlantilla = $("#plantilla")
            , jFactura = $("#factura")
            , jLoading = $("#loading");
        /******************************************************************/

        scope.show = function (id) {
            jInicio.hide();
            jCliente.hide();
            jServicio.hide();
            jUsuario.hide();
            jPlantilla.hide();
            jLoading.hide();
            jFactura.hide();

            if (id === "inicio") {
                scope.inicio();
                jInicio.show();
            }
            else if (id === "cliente") {
                $("a[href=#cliListado]").click();
                scope.todos = [];
                jCliente.show();
            }
            else if (id === "servicio") {
                $rootScope.mostrarErrores();
                jServicio.show();
            }
            else if (id === "user") {
                jUsuario.show();
            }
            else if (id === "plantilla") {
                $rootScope.themplete();
                jPlantilla.show();
            }
            else if (id === "factura") {
                scope.facturas();
                jFactura.show();
            }
        }

        /*Metodos Inicio*/
        scope.inicio = function () {
            jLoading.show();
            $http.post(serv.url + "inicio", {}).then(function (data) {
                console.log(data.data.d);
                if (data.data.d !== "") {
                    var val = JSON.parse(data.data.d);
                    scope.iniClientes = val[0].ClientesRegistrados;
                    scope.iniError = val[0].Errores;
                    scope.iniFactTest = val[0].FacturasTesting;
                    scope.iniCredTest = val[0].CreditoTesting;
                    scope.iniFactInsp = val[0].FacturasInspeccion;
                    scope.iniCredInsp = val[0].CreditoInspeccion;
                }
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde : " + data.data);
            });
            jLoading.hide();
        }

        /*Metodos Clientes*/
        scope.TodosClientes = function () {
            jLoading.show();
            $http.post(serv.url + "MuestraClientes", {}).then(function (data) {
                console.log(data.data);
                scope.todos = JSON.parse(data.data.d);
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde :" + data.data);
            });
            jLoading.hide();
        }

        scope.keyBuscarCli = function (key) {
            console.log(key);
            if (key.key === "Enter") {
                console.log(key.key);
                scope.buscarClientes();
            }
        }

        scope.buscarClientes = function () {
            jLoading.show();
            var param = new Object();
            param.val = scope.bCliente;
            $http.post(serv.url + "buscarEnClientes", JSON.stringify(param)).then(function (data) {
                console.log(data.data);
                if (data.data.d === "") {
                    serv.msgError("Lo sentimos no existe algun registro con el valor deseado " + data.data);
                }
                else {
                    scope.todos = JSON.parse(data.data.d);
                }

            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        /*Metodos Errores*/
        $rootScope.mostrarErrores = function () {
            jLoading.show();
            $http.post(serv.url + "MuestraErrores", {}).then(function (data) {
                //console.log(data.data.d);
                scope.errores = JSON.parse(data.data.d);
                $("a[href=#servListado]").click();
                //console.log(scope.errores);
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde");
            });
            jLoading.hide();
        }

        scope.buscarErrores = function () {
            jLoading.show();
            var param = new Object();
            param.val = scope.bErrores;
            $http.post(serv.url + "buscarEnArchivos", JSON.stringify(param)).then(function (data) {
                console.log(data.data);
                if (data.data.d === "") {
                    serv.msgError("Lo sentimos no existe algun registro con el valor deseado " + data.data);
                }
                else {
                    scope.errores = JSON.parse(data.data.d);
                }

            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        /*Metodos Plantillas*/
        $rootScope.themplete = function () {
            jLoading.show();
            $("#temNuevo").show();
            $("#temEditar").hide();
            scope.plantillas = [];
            $http.post(serv.url + "listadoTempletes", {}).then(function (data) {
                scope.plantillas = JSON.parse(data.data.d);
                console.log(scope.plantillas);
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        /*Metodos Facturas*/
        scope.facturas = function () {
            jLoading.show();
            $http.post(serv.url + "facturas", {}).then(function (data) {
                console.log(data.data.d);
                var res = JSON.parse(data.data.d);
                scope.noenviadas = res.noenviados;
                console.log(scope.noenviadas);
                scope.enviadas = res.enviados;
                console.log(scope.enviadas);
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        scope.buscarFactEnvi = function () {
            jLoading.show();
            var param = new Object();
            param.val = scope.bEnviadas;
            $http.post(serv.url + "buscarEnEnviadas", JSON.stringify(param)).then(function (data) {
                console.log(data.data);
                if (data.data.d === "") {
                    serv.msgError("Lo sentimos no existe algun registro con el valor deseado " + data.data);
                }
                else {
                    scope.enviadas = JSON.parse(data.data.d);
                }

            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        scope.buscarFactNoEnvi = function () {
            jLoading.show();
            var param = new Object();
            param.val = scope.bNoEnviadas;
            $http.post(serv.url + "buscarEnNoEnviadas", JSON.stringify(param)).then(function (data) {
                console.log(data.data);
                if (data.data.d === "") {
                    serv.msgError("Lo sentimos no existe algun registro con el valor deseado " + data.data);
                }
                else {
                    scope.noenviadas = JSON.parse(data.data.d);
                }

            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        /*Metodos Fin*/

        scope.show("inicio");
    })
    .controller('ctrlCliente', function ($http, serv) {
        var scope = this;
        scope.todos = [];
        scope.uno = [];
        /*############################################*/
        scope.Id_Cliente = "";
        scope.Nombre = "";
        scope.RFC = "";
        scope.FormatoCoreo = 0;
        scope.AplicaContrato = 0;
        scope.listaFormato = {};
        scope.nuevoMail = "";
        scope.mails = [];
        scope.buscarVal = "";
        /*############################################*/
        /******************************************************************/
        /*Controles jquery*/
        var jLoading = $("#loading")
            , jBtnGuardarCliente = $("button#guardarCli")
            , jBtnEnviarCliente = $("button#enviarCli")
            , jverClienteListado = $("a[href=#cliListado]")
            , jverClienteEditar = $("a[href=#cliEditar]")
            , jverClienteAsignar = $("a[href=#cliAsignar]");

        /******************************************************************/
        jBtnGuardarCliente.hide();
        jBtnEnviarCliente.hide();

        scope.UnCliente = function (id, rfc) {

            nuevoMail = [];
            jLoading.show();
            var params = new Object();
            params.id_Cliente = id;
            params.RFC = rfc;
            params = JSON.stringify(params);

            $http.post(serv.url + "MuestraClientesSingular", params).then(function (data) {
                console.log(data);
                if (data.data.d != "") {
                    scope.uno = JSON.parse(data.data.d);
                    console.log(scope.uno);

                    scope.Id_Cliente = id;
                    scope.Nombre = scope.uno.Nombre;
                    scope.RFC = scope.uno.RFC;
                    $("#cAplicaContrato option[value=" + scope.uno.AplicaContrato + "]").attr("selected", true);

                    $("select#cFormatoCoreo option[value$='" + scope.uno.FormatoCoreo + "']").attr("selected", true);

                    console.log(scope.FormatoCoreo);
                    scope.Correo = scope.uno.Correo;
                    scope.mails = scope.uno.Correo;

                    jBtnGuardarCliente.show();
                    jBtnEnviarCliente.show();
                    jverClienteEditar.click();
                }
                else { serv.msgError("No se obtuvo ningun resultado"); }
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos existio un error intentelo mas tarde " + data.data);
            });
            //scope.cargarFormatos();
            jLoading.hide();
        }

        scope.guardar = function () {
            jLoading.show();
            var param = new Object()
            param.contrato = scope.AplicaContrato;
            param.RFC = scope.RFC;
            param.formato = scope.FormatoCoreo;
            $http.post(serv.url + "editarCliente", JSON.stringify(param)).then(function (data) {
                console.log(data.data);
                serv.msgEcho("los valores fueron agregados correctamente");
                jverClienteListado.click();
                //scope.FormatoCoreo = 0;
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        scope.keyMail = function (evento) {
            if (evento.key == " " || evento.key == "Enter") {

                expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                console.log(expr.test(scope.nuevoMail));
                if (expr.test(scope.nuevo)) {
                    scope.mails.push(scope.nuevo);
                }
                scope.nuevo = "";
            }
        }

        scope.cargarCorreosAsignados = function () {
            jLoading.show();
            if (scope.RFC != "") {
                var param = new Object()
                param.rfc = scope.RFC;
                $http.post(serv.url + "consultarAsignarMail", JSON.stringify(param)).then(function (data) {
                    console.log(data.data);
                    if (data.data.d != "") {
                        scope.mails = data.data.d;
                    }
                    else {
                        scope.mails = ["ejemplo@ejemplo.com"];
                    }
                }, function (data) { serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data); });
            }
            else { serv.msgError("Tiene que seleccionar un cliente para poder agregarle un nuevo correo " + data.data); }
            jLoading.hide();
        }

        scope.asignar = function () {
            jLoading.show();
            if (scope.RFC != "") {
                var param = new Object()
                param.rfc = scope.RFC;
                param.newcorreo = scope.mails;
                console.log(param);
                $http.post(serv.url + "asignarMail", JSON.stringify(param)).then(function (data) {
                    console.log(data.data);
                    if (data.data.d == "listo") {
                        serv.msgEcho("los valores fueron agregados correctamente");
                        scope.mails = [];
                    }

                    if (data.data.d == "todo bien") {
                        jBtnGuardarCliente.hide();
                        jBtnEnviarCliente.hide();

                        jverClienteListado.click();
                        serv.msgEcho("Realizado con exito");
                    } else { serv.msgError("Existio un error " + data.data); }

                }, function (data) {
                    console.log(data);
                    serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
                });
            }
            else { serv.msgError("Tiene que seleccionar un cliente para poder agregarle un nuevo correo " + data.data); }
            jLoading.hide();
        }

        scope.cargarFormatos = function () {
            jLoading.show();
            $http.post(serv.url + "obtenerPlantillas", {}).then(function (data) {
                console.log(data.data);
                scope.listaFormato = JSON.parse(data.data.d);
                console.log(scope.listaFormato);
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        scope.regresar = function () {
            jverClienteListado.click();
        }

        scope.quitarMail = function (indx) {
            scope.mails.splice(indx, 1);
        }

    })
    .controller('ctrlServicio', function ($http, serv, $rootScope) {
        var scope = this;
        scope.archivo = "";
        scope.contenido = "";
        scope.folioError = "";
        scope.error = "";

        /******************************************************************/
        /*Controles jquery*/
        var jLoading = $("#loading")
            , jBtnGuardarServ = $("button#servGuardar")
            , jBtnReiniciarServ = $("button#servReiniciar")
            , verServicioListado = $("a[href=#servListado]")
            , verServicioEditar = $("a[href=#servEditado]");

        scope.buscarTxt = function (txt, id, error) {
            jLoading.show();
            var param = new Object();
            param.archivo = txt;
            param = JSON.stringify(param);

            scope.contenido = "";
            scope.error = "";

            $http.post(serv.url + "cargarArchivo", param).then(function (data) {
                console.log(data.data);
                scope.contenido = data.data.d;
                scope.archivo = txt;
                scope.folioError = id;
                scope.error = error;
                jBtnGuardarServ.show();
                jBtnReiniciarServ.show();

                verServicioEditar.click();

            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        scope.guardar = function () {
            jLoading.show();
            var params = new Object();
            params.archivo = scope.archivo;
            params.contenido = scope.contenido;
            params = JSON.stringify(params);
            $http.post(serv.url + "guardarArchivo", params).then(function (data) {
                console.log(data.data);
                if (data.data.d == "modificacion") {
                    $rootScope.mostrarErrores();
                    serv.msgEcho("Los datos se ingresaron de manera correcta");
                    jBtnGuardarServ.hide();
                    jBtnReiniciarServ.hide();
                    verServicioListado.click();
                }
                else if (data.data.d.indexOf("error") != -1) {
                    if (data.data.d.length > 5) {
                        serv.msgError(data.data.d);
                    }
                    else {
                        serv.msgError("El archivo no existe o fue movido, reprocese el archivo o verifique con el adminisrador del sistema.");
                    }
                    $rootScope.mostrarErrores();
                    verServicioListado.click();
                }
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });

            jLoading.hide();
        }

        scope.reiniciarServicio = function () {
            jLoading.show();

            var params = new Object();
            params.idError = scope.folioError;
            params.archivo = scope.archivo;
            params = JSON.stringify(params);

            $http.post(serv.url + "ReiniciaServicio", params).then(function (data) {
                console.log(data.data);
                serv.msgEcho("El servicio fue reiniciado de manera correcta");
                $rootScope.mostrarErrores();
                verServicioListado.click();
            }, function (data) {
                console.log(data);
                $rootScope.mostrarErrores();
                verServicioListado.click();
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde", +data.data);
            });
            jLoading.hide();
        }
    })
    .controller('ctrlPlantilla', function ($http, serv, $rootScope) {
        var scope = this;
        scope.id = 0;
        scope.asunto = "";
        scope.nombre = "";
        scope.cuerpo = "";
        scope.bandera = 0;

        /******************************************************************/
        /*Controles jquery*/
        var jLoading = $("#loading")
            , btnPlantillaEditar = $("button#temEditar")
            , btnPlantillaNuevo = $("button#temNuevo")
            , verPlantillaListado = $("a[href=#planListado]")
            , verPlantillaEditado = $("a[href=#planEditado]")
            , verPlantillaNueva = $("a[href=#planNuevo]");

        scope.cargarhemplete = function (id) {
            var params = new Object();
            params.id = id;
            $http.post(serv.url + "obtenerTemplete", JSON.stringify(params)).then(function (data) {
                scope.themplete = JSON.parse(data.data.d);
                scope.id = id;
                var html = JSON.parse(data.data.d);
                html = html[0].Cuerpo;
                CKEDITOR.instances.editor.setData(html);

                scope.asunto = scope.themplete[0].Asunto;
                scope.nombre = scope.themplete[0].Nombre
                scope.bandera = 3;
                btnPlantillaEditar.show();
                btnPlantillaNuevo.show();
                verPlantillaEditado.click();
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });
        }

        scope.editarThemplete = function () {
            var params = new Object();
            params.id = scope.id;
            params.nombre = scope.nombre;
            params.asunto = scope.asunto;
            params.cuerpo = CKEDITOR.instances['editor'].getData();
            params.bandera = 3;
            console.log(params);
            scope.guardar(params);
        }

        scope.nuevoThemplete = function () {
            var params = new Object();
            params.id = 0;
            params.nombre = scope.nombre;
            params.asunto = scope.asunto;
            params.cuerpo = CKEDITOR.instances['nuevo'].getData();
            params.bandera = 2;
            console.log(params);
            scope.guardar(params);
        }

        scope.guardar = function (obj) {
            $http.post(serv.url + "modificarTemplete", JSON.stringify(obj)).then(function (data) {
                console.log(data.data.d);
                verPlantillaListado.click();
                serv.msgEcho("Datos insertados Correctamente");
                $rootScope.themplete();
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });
        }

        scope.regresarLista = function () {
            btnPlantillaEditar.hide();
            btnPlantillaNuevo.hide();
            verPlantillaListado.click();
        }

        scope.deleteThemplete = function () {
            var params = new Object();
            params.id = scope.id;
            $http.post(serv.url + "deleteTemplete", JSON.stringify(params)).then(function (data) {
                console.log(data.data.d);
                verPlantillaListado.click();
                serv.msgEcho("Plantilla eliminada de forma correcta");
                $rootScope.themplete();
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });
        }

    })
    .controller('ctrlFactura', function ($http, serv) {
        var scope = this;
        scope.plantilla = [];
        scope.asunto = "";
        scope.cuerpo = "";
        scope.cliente = "";
        scope.archivo = [];
        scope.correos = [];
        scope.rfc = 0;
        scope.nuevo = "";
        scope.files;

        /******************************************************************/
        /*Controles jquery*/
        var jLoading = $("#loading")
            , jverFacturaEnviada = $("a[href=#factEnviadas]")
            , jverFacturaNoEnviada = $("a[href=#factNoEnviadas]")
            , jverFacturaPrev = $("a[href=#factPre]");

        scope.buscarFactura = function (id) {
            jLoading.show();
            var params = new Object();
            params.id = id;
            $http.post(serv.url + "vistaPrevia", JSON.stringify(params)).then(function (data) {
                console.log(data);
                scope.plantilla = JSON.parse(data.data.d);
                scope.asunto = scope.plantilla.a[0].Asunto;

                var html = JSON.parse(data.data.d);
                html = html.a[0].Cuerpo;
                CKEDITOR.instances.correos.setData(html);

                scope.cuerpo = scope.plantilla.a[0].Cuerpo;
                scope.cliente = scope.plantilla.a[0].RFC + " - " + scope.plantilla.a[0].Nombre1;
                scope.archivo = scope.plantilla.c;
                /////////////////////////////
                scope.rfc = scope.plantilla.a[0].RFC
                console.log(scope.plantilla.b);
                scope.correos = scope.plantilla.b;
                console.log(scope.correos);
                jverFacturaPrev.click();
                $("button#factEnviar").show();
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }

        /*
        scope.enviar = function () {
            jLoading.show();
    
            var x = new Object();
            x.correos = scope.correos;
            x.asunto = scope.asunto;
            x.cuerpo = CKEDITOR.instances.correos.getData();
            x.rfc = scope.rfc;
            x.archivos = scope.archivo;
    
            $http.post(serv.url + "enviarMail", {JSON.stringify(x)}).then(function (data) {//
                console.log(data.data);
                
                serv.msgEcho("Se envio el correo correctamente");
                $("#factPre").removeClass("active").removeClass("in").addClass("fade");
                $("#factEnviadas").removeClass("active").removeClass("in").addClass("fade");
                $("#factNoEnviadas").addClass("active").addClass("in").removeClass("fade");
    
            }, function (data) {
                console.log(data);
                serv.msgError("Lo sentimos, existio un error porfavor intentelo mas tarde " + data.data);
            });
            jLoading.hide();
        }*/

        scope.regresar = function () {
            jverFacturaNoEnviada.click();

        }

        scope.keyMail = function (evento) {
            if (evento.key === " " || evento.key === "Enter") {
                expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (expr.test(scope.nuevo)) {
                    console.log(scope.nuevo);
                    scope.correos.push(JSON.parse("{\"Correo\":\"" + scope.nuevo + "\"}"));
                    console.log(scope.correos);
                }
                scope.nuevo = "";
            }
        }

        scope.quitarMail = function (indx) {
            scope.correos.splice(indx, 1);
        }


        scope.cargarFiles = function () {
            var archivo = $("#factEnviar");
            var lista = archivo[0].files;
            console.log(lista);
        }
    })
    .service('serv', function ($http) {
        var scope = this;
        /******************************************************************/
        /*Clientes*/
        scope.todos = [];
        /*Servicios*/
        scope.errores = [];
        /*Plantillas*/
        scope.plantillas = [];
        /******************************************************************/

        scope.msgError = function (msg) {
            $.notify({
                title: "Error",
                message: msg
            }, {
                    type: "danger",
                    delay: 60000
                });
        }

        scope.msgEcho = function (msg) {
            $.notify({
                title: "Listo",
                message: msg
            }, {
                    type: "success"
                });
        }

        scope.url = "webServices.aspx/";
    });

