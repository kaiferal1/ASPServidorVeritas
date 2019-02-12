<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="ASPSERVIDORVeritas.web.dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
    <meta name="viewport" content="width=device-width" />
    <title>Facturación CDFI 3.3</title>
    <link rel="apple-touch-icon" sizes="76x76" href="assets/img/apple-icon.png" />
    <link rel="icon" type="image/png" href="assets/img/favicon.png" />
    <!-- Bootstrap core CSS     -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <!--  Material Dashboard CSS    -->
    <link href="assets/css/material-dashboard.css?v=1.2.0" rel="stylesheet" />
    <!--  CSS for Demo Purpose, don't include it in your project     -->
    <link href="assets/css/demo.css" rel="stylesheet" />
    <link href="assets/css/index.css" rel="stylesheet" />
    <!--     Fonts and icons     -->
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet">
    <link href='http://fonts.googleapis.com/css?family=Roboto:400,700,300|Material+Icons' rel='stylesheet' type='text/css'>

    <link href="assets/ckeditor/samples/css/samples.css" rel="stylesheet" />
    <link href="assets/ckeditor/samples/toolbarconfigurator/lib/codemirror/neo.css" rel="stylesheet" />
    <link href="assets/css/dropzone.css" rel="stylesheet" />
</head>

<body data-ng-app="mdlAppWeb" data-ng-controller="ctrlnav as nav">
    <div id='loading' data-ng-class="nav.clsload">
        <img src='assets/css/media/images/loader.gif' />
    </div>

    <div class="wrapper">
        <!-- Inicio Menu de Navegación-->
        <div class="sidebar" data-color="red">
            <!--data-image="main_assets/img/sidebar-3.jpg"-->
            <!-- Tip 1: You can change the color of the sidebar using: data-color="purple | blue | green | orange | red" / Tip 2: you can also add an image using data-image tag -->
            <div class="logo">
                <a class="simple-text">
                    Facturación CFDI 3.3
                </a>
            </div>
            <div class="sidebar-wrapper">
                <ul class="nav">
                    <li data-ng-click="nav.show('inicio')">
                        <a href="#inicio">
                            <i class="material-icons">dashboard</i>
                            <p>Inicio</p>
                        </a>
                    </li>
                    <li data-ng-click="nav.show('cliente')">
                        <a href="#cliente">
                            <i class="material-icons">person</i>
                            <p>Clientes</p>
                        </a>
                    </li>
                    <li data-ng-click="nav.show('servicio')">
                        <a href="#servicio">
                            <i class="material-icons">content_paste</i>
                            <p>Servicio</p>
                        </a>
                    </li>
                    <li data-ng-click="nav.show('plantilla')">
                        <a href="#plantilla">
                            <i class="material-icons">mail_outline</i>
                            <p>Plantillas</p>
                        </a>
                    </li>
                    <li data-ng-click="nav.show('factura')">
                        <a href="#factura" id="liAFacturas">
                            <i class="material-icons">content_paste</i>
                            <p>Facturas</p>
                        </a>
                    </li>
                    <li>
                        <a href=".\login.html" class="dropdown-toggle">
                            <i class="material-icons">exit_to_app</i>
                            <p>Salir</p>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <!-- Fin Menu de Navegación-->

        <div class="main-panel">
            <!-- Inicio Header-->
            <nav class="navbar navbar-transparent navbar-absolute">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    </div>

                    <div class="collapse navbar-collapse">
                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <p class="navbar-brand">Bienvenido, </p>
                            </li>
                            <li>
                                <a href=".\login.html" class="dropdown-toggle">
                                    <i class="material-icons">exit_to_app</i>
                                    <p class="hidden-lg hidden-md">Salir</p>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            <!-- Inicio Contenido-->
            <div class="content">
                <!-- Inicio-->
                <div id="inicio">
                    <div class="col-lg-4 col-md-6 col-sm-6">
                        <div class="card card-stats">
                            <div class="card-header" data-background-color="orange">
                                <i class="material-icons">content_copy</i>
                            </div>
                            <div class="card-content">
                                <p class="category">Clientes Registrados</p>
                                <h3 class="title">
                                    {{nav.iniClientes}}
                                </h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 col-sm-6">
                        <div class="card card-stats">
                            <div class="card-header" data-background-color="green">
                                <i class="material-icons">store</i>
                            </div>
                            <div class="card-content">
                                <p class="category">Errores Generados Por Mes</p>
                                <h3 class="title">{{nav.iniError}}</h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 col-sm-6">
                        <div class="card card-stats">
                            <div class="card-header" data-background-color="red">
                                <i class="material-icons">info_outline</i>
                            </div>
                            <div class="card-content">
                                <p class="category">Facturas Testing</p>
                                <h3 class="title">{{nav.iniFactTest}}</h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 col-sm-6">
                        <div class="card card-stats">
                            <div class="card-header" data-background-color="blue">
                                <i class="fa fa-twitter"></i>
                            </div>
                            <div class="card-content">
                                <p class="category">Credito Testing</p>
                                <h3 class="title">{{nav.iniCredTest}}</h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 col-sm-6">
                        <div class="card card-stats">
                            <div class="card-header" data-background-color="red">
                                <i class="material-icons">info_outline</i>
                            </div>
                            <div class="card-content">
                                <p class="category">Factura Inspeccionada</p>
                                <h3 class="title">{{nav.iniFactInsp}}</h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 col-sm-6">
                        <div class="card card-stats">
                            <div class="card-header" data-background-color="blue">
                                <i class="fa fa-twitter"></i>
                            </div>
                            <div class="card-content">
                                <p class="category">Credito Inspeccionada</p>
                                <h3 class="title">{{nav.iniCredInsp}}</h3>
                            </div>
                        </div>
                    </div>
                </div>
              <!-- Cliente-->
                <div class="row" id="cliente" data-ng-controller="ctrlCliente as cli" data-init="cli.cargarFormatos()">
                    <ul class="nav nav-tabs" style="background-color:#797979">
                        <li class="active"><a data-toggle="tab" href="#cliListado">Listado Clientes</a></li>
                        <li><a data-toggle="tab" href="#cliEditar" style="display:none;">Editar Clientes</a></li>
                        <li><a data-toggle="tab" href="#cliAsignar">Asignar Correo</a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="cliListado" class="tab-pane fade active in">
                            <div class="col-lg-12 col-md-12">
                                <div class="card">
                                    <div class="card-header" style="background-color:#a90329">
                                        <h4 class="title">Clientes</h4>
                                        <p class="category">Todos los Clientes</p>
                                    </div>
                                    <div class="card-content table-responsive">
                                        <div class="form-group">
                                            <div class="col-sm-10">
                                                <input type="search" class="form-control" id="cbuscar" placeholder="Buscar..." data-ng-model="nav.bCliente" data-ng-keypress="nav.keyBuscarCli($event)">
                                            </div>
                                            <span class="col-sm-1 control-label" style="cursor:pointer" data-ng-click="nav.buscarClientes()"><i class="material-icons">search</i></span>
                                            <span class="col-sm-1 control-label" style="cursor:pointer" data-ng-click="nav.TodosClientes()">Mostrar Todo</span>
                                        </div>
                                        <table class="table table-hover">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th>NOMBRE</th>
                                                    <th>RFC</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr data-ng-repeat="item in nav.todos" id="{{$index}}" data-ng-click="cli.UnCliente(item.Id_Cliente,item.RFC)">
                                                    <td>{{item.Nombre}}</td>
                                                    <td>{{item.RFC}}</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="cliEditar" class="tab-pane fade">
                            <div class="col-lg-12 col-md-12">
                                <div class="card">
                                    <div class="card-header" style="background-color:#a90329">
                                        <h4 class="title">Editar Cliente</h4>
                                        <p class="category">Datos de forma detallada</p>
                                    </div>
                                    <div class="card-content table-responsive">
                                        <div class="form-group">
                                            <label for="cNombre" class="col-sm-2 control-label">Nombre :</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" id="cNombre" placeholder="Nombre" value="{{cli.uno.Nombre}}" data-ng-model="cli.Nombre" readonly />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="cRFC" class="col-sm-2 control-label">RFC :</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" id="cRFC" placeholder="RFC" value="{{cli.RFC}}" data-ng-model="cli.RFC" readonly />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="cMails" class="col-sm-2 control-label">Correos :</label>
                                            <div class="col-sm-10">
                                                <span class="tags" data-ng-repeat="item in cli.Correo track by $index">
                                                    <span class="label label-info">
                                                        {{item}}
                                                    </span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="cAplicaContrato" class="col-sm-2 control-label">Aplica Contrato :</label>
                                            <div class="col-sm-10">
                                                <select data-ng-model="cli.AplicaContrato" class="form-control" id="cAplicaContrato">
                                                    <option value="1">Aplica</option>
                                                    <option value="0">No Aplica</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="cFormatoCoreo" class="col-sm-2 control-label">Formato Correo :</label>
                                            <div class="col-sm-10">
                                                <select data-ng-model="cli.FormatoCoreo" class="form-control" id="cFormatoCoreo" data-ng-options="item.iD_Template as item.Nombre for item in cli.listaFormato">
                                                    <option value="" >--Elige opcion--</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-10">
                                                <button type="submit" id="" class="btn btn-default" data-ng-click="cli.regresar()">Regresar</button>
                                                <button type="submit" id="guardarCli" class="btn btn-default" data-ng-click="cli.guardar()">Guardar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="cliAsignar" class="tab-pane fade">
                            <div class="col-lg-12 col-md-12">
                                <div class="card">
                                    <div class="card-header" style="background-color:#a90329">
                                        <h4 class="title">Asignar correo a Cliente</h4>
                                        <p class="category"></p>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="cNombre" class="col-sm-2 control-label">Nombre :</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="cNombre" placeholder="Nombre" value="{{cli.uno.Nombre}}" data-ng-model="cli.Nombre" readonly />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="cRFC" class="col-sm-2 control-label">RFC :</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="FRFC" placeholder="RFC" value="{{cli.RFC}}" data-ng-model="cli.RFC" readonly />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="cMails" class="col-sm-2 control-label">Correos :</label>
                                    <div class="col-sm-10">
                                        <span class="tags" data-ng-repeat="item in cli.mails track by $index">
                                            <span class="label label-info">
                                                {{item}}
                                                <i class="fa fa-times" style="cursor:pointer" data-ng-click="cli.quitarMail($index)"></i>
                                            </span>
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="cCorreo" class="col-sm-2 control-label">Nuevo Correo :</label>
                                    <div class="col-sm-10">
                                        <input type="email" class="form-control" id="cCorreo" placeholder="Presione Enter o espacio para agregar un nuevo correo" value="{{cli.nuevo}}" data-ng-model="cli.nuevo" data-ng-keypress="cli.keyMail($event)" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-10">
                                        <button type="submit" id="correoCli" class="btn btn-default" data-ng-click="cli.asignar()">Guardar Correos</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--Servicio-->
                <div id="servicio" class="row" data-ng-controller="ctrlServicio as serv">
                    <ul class="nav nav-tabs" style="background-color:#797979">
                        <li class="active"><a data-toggle="tab" href="#servListado">Listado Servicios</a></li>
                        <li><a data-toggle="tab" href="#servEditado">Editar Servicio</a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="servListado" class="tab-pane fade active in">
                            <div id="sTabla" class="col-lg-12 col-md-12">
                                <div class="card">
                                    <div class="card-header" style="background-color:#a90329">
                                        <h4 class="title">Servicio</h4>
                                        <p class="category">LISTADO DE ERRORES DEL SERVICIO</p>
                                    </div>
                                    <div class="card-content table-responsive">
                                        <div class="form-group">
                                            <div class="col-sm-8">
                                                <input type="search" class="form-control" id="cbuscar" placeholder="Buscar..." data-ng-model="nav.bErrores">
                                            </div>
                                            <span class="col-sm-2 control-label" style="cursor:pointer" data-ng-click="nav.buscarErrores()"><i class="material-icons">search</i></span>
                                        </div>
                                        <table class="table table-hover">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th>FOLIO ERROR</th>
                                                    <th>DESCRIPCION</th>
                                                    <th>ESTATUS PROCESO</th>
                                                    <th>ARCHIVO</th>
                                                    <th>FECHA</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="{{$index}}" data-ng-click="serv.buscarTxt(item.Archivo,item.FOLIO,item.DESCRIPCION)" data-ng-repeat="item in nav.errores track by $index">
                                                    <td>{{item.FOLIO}}</td>
                                                    <td>{{item.DESCRIPCION}}</td>
                                                    <td>{{item.ESTATUS}}</td>
                                                    <td>{{item.Archivo}}</td>
                                                    <td>{{item.Fecha}}</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="servEditado" class="tab-pane fade">
                            <div id="sArchivo" class="col-lg-12 col-md-12">
                                <div class="card">
                                    <div class="card-header" style="background-color:#a90329">
                                        <h4 class="title">Archivo</h4>
                                        <p class="category">ERROR : </p>
                                    </div>
                                    <div class="card-content table-responsive">
                                        <%--<div class="buscar">
                                            <div class="col-lg-6 col-md-6 col-sm-3 col-xs-3">
                                                <input type="search" class="form-control" id="cbuscar" placeholder="Buscar..." data-ng-model="nav.bErrores">
                                                <span data-ng-click="nav.buscarErrores()"><i class="material-icons">search</i></span>
                                            </div>
                                        </div>--%>
                                        <div class="form-group">
                                            <label for="sArchivo" class="col-sm-1 control-label">Contenido :</label>
                                            <div class="col-sm-11">
                                                <textarea class="form-control" id="sArchivo" placeholder="Contenido" data-ng-model="serv.contenido"></textarea>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-10">
                                                <button type="submit" id="servGuardar" class="btn btn-default" data-ng-click="serv.guardar()">Guardar</button>
                                                <button type="submit" id="servReiniciar" class="btn btn-default" data-ng-click="serv.reiniciarServicio()">Reprocesar Archivo</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Plantillas-->
                <div id="plantilla" class="row" data-ng-controller="ctrlPlantilla as temp">
                    <div class="card">
                        <form class="form-horizontal" role="form">
                            <ul class="nav nav-tabs" style="background-color:#797979">
                                <li class="active"><a data-toggle="tab" href="#planListado">Listado Plantillas</a></li>
                                <li><a data-toggle="tab" href="#planEditado" style="display:none;">Editar Plantilla</a></li>
                                <li><a data-toggle="tab" href="#planNuevo">Nueva Plantilla</a></li>
                            </ul>
                            <div class="tab-content">
                                <div id="planListado" class="tab-pane fade active in">
                                    <div id="sTabla" class="col-lg-12 col-md-12">
                                        <div class="card">
                                            <div class="card-header" style="background-color:#a90329">
                                                <h4 class="title">Plantillas</h4>
                                                <p class="category"></p>
                                            </div>
                                            <div class="card-content table-responsive">
                                                <table class="table table-hover">
                                                    <thead class="text-warning">
                                                        <tr>
                                                            <th>Id</th>
                                                            <th>Nombre</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr data-ng-click="temp.cargarhemplete(item.iD_Template)" data-ng-repeat="item in nav.plantillas track by $index">
                                                            <td>{{item.iD_Template}}</td>
                                                            <td>{{item.Nombre}}</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="planEditado" class="tab-pane fade">
                                    <div id="plan" class="col-lg-12 col-md-12">
                                        <br />
                                        <div class="form-group">
                                            <label for="tNombre" class="col-sm-2 control-label">Nombre :</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" id="tNombre" placeholder="Nombre de la Plantilla" value="{{temp.nombre}}" data-ng-model="temp.nombre" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="tAsunto" class="col-sm-2 control-label">Asunto :</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" id="tAsunto" placeholder="Asunto para la plantilla" value="{{temp.asunto}}" data-ng-model="temp.asunto" />
                                            </div>
                                        </div>
                                        <div class="adjoined-bottom">
                                            <div class="grid-container">
                                                <div class="grid-width-100">
                                                    <textarea  id="editor" name="editor">

                                                    </textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                              <div class="col-sm-offset-2 col-sm-10">
                                                  <button type="button" id="btnRegresar" class="btn btn-default" data-ng-click="temp.regresarLista()">Regresar</button>
                                                  <button type="submit" id="temEditar" class="btn btn-default" data-ng-click="temp.editarThemplete()">Guardar Plantilla</button>
                                                  <button type="submit" id="temDelete" class="btn btn-default" data-ng-click="temp.deleteThemplete()">Eliminar Plantilla</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="planNuevo" class="tab-pane fade">
                                    <div id="plan" class="col-lg-12 col-md-12">
                                        <br />
                                        <div class="form-group">
                                            <label for="tnNombre" class="col-sm-2 control-label">Nombre :</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" id="tnNombre" placeholder="Nombre de la Plantilla" value="{{temp.nombre}}" data-ng-model="temp.nombre" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="tnAsunto" class="col-sm-2 control-label">Asunto :</label>
                                            <div class="col-sm-10">
                                                <input type="text" class="form-control" id="tnAsunto" placeholder="Asunto para la plantilla" value="{{temp.asunto}}" data-ng-model="temp.asunto" />
                                            </div>
                                        </div>
                                        <div class="adjoined-bottom">
                                            <div class="grid-container">
                                                <div class="grid-width-100">
													<textarea  id="nuevo" name="nuevo">

                                                    </textarea>
                                                </div>
                                            </div>
                                        </div>
                                       <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-10">
                                                <button type="button" id="btnRegresar" class="btn btn-default" data-ng-click="temp.regresarLista()">Regresar</button>
                                                <button type="submit" id="tnNuevo" class="btn btn-default" data-ng-click="temp.nuevoThemplete()">Guardar Nueva Plantilla</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                <!--Facturas-->
                <div id="factura" class="row" data-ng-controller="ctrlFactura as fact">
                    <ul class="nav nav-tabs" style="background-color:#797979">
                        <li class="active"><a data-toggle="tab" href="#factNoEnviadas">Listado de Facturas No Enviadas</a></li>
                        <li><a data-toggle="tab" href="#factEnviadas">Listado de Facturas Enviadas</a></li>
                        <li style="display:none"><a data-toggle="tab" href="#factPre">Previsualizacion del Correo</a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="factNoEnviadas" class="tab-pane fade active in">
                            <div id="sTabla" class="col-lg-12 col-md-12">
                                <div class="card">
                                    <div class="card-header" style="background-color:#a90329">
                                        <h4 class="title">Facturas No Enviadas</h4>
                                        <p class="category">Listado de facturas que aun no se envian</p>
                                    </div>
                                    <div class="card-content table-responsive">
                                        <div class="form-group">
                                            <div class="col-sm-10">
                                                <input type="search" class="form-control" id="cbuscar" placeholder="Buscar..." data-ng-model="nav.bNoEnviadas" />
                                            </div>
                                            <span class="col-sm-2 control-label" style="cursor:pointer" data-ng-click="nav.buscarFactNoEnvi()"><i class="material-icons">search</i></span>
                                        </div>
                                        <table class="table table-hover">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th>IDENTIFICADOR</th>
                                                    <th>NOMBRE ARCHIVO</th>
                                                    <th>NOMBRE CLIENTE</th>
                                                    <th>RFC</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="{{$index}}" data-ng-click="fact.buscarFactura(item.idArchivo)" data-ng-repeat="item in nav.noenviadas track by $index">
                                                    <td>{{item.idArchivo}}</td>
                                                    <td>{{item.aNombre}}</td>
                                                    <td>{{item.cNombre}}</td>
                                                    <td>{{item.RFC}}</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="factEnviadas" class="tab-pane fade ">
                            <div id="sTabla" class="col-lg-12 col-md-12">
                                <div class="card">
                                    <div class="card-header" style="background-color:#a90329">
                                        <h4 class="title">Facturas Enviadas</h4>
                                        <p class="category">Listado de facturas que ya se enviaron</p>
                                    </div>
                                    <div class="card-content table-responsive">
                                        <div class="form-group">
                                            <div class="col-sm-10">
                                                <input type="search" class="form-control" id="cbuscar" placeholder="Buscar..." data-ng-model="nav.bEnviadas" />
                                            </div>
                                            <span class="col-sm-2 control-label" style="cursor:pointer" data-ng-click="nav.buscarFactEnvi()"><i class="material-icons">search</i></span>
                                        </div>
                                        <table class="table table-hover">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th>IDENTIFICADOR</th>
                                                    <th>NOMBRE ARCHIVO</th>
                                                    <th>NOMBRE CLIENTE</th>
                                                    <th>FECHA y HORA</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="{{$index}}" data-ng-click="fact.buscarFactura(item.idArchivo)" data-ng-repeat="item in nav.enviadas track by $index">
                                                    <td>{{item.idArchivo}}</td>
                                                    <td>{{item.aNombre}}</td>
                                                    <td>{{item.cNombre}}</td>
                                                    <td>{{item.fechaArchivo}}</td>

                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="factPre" class="tab-pane fade">
                            <div id="sArchivo" class="col-lg-12 col-md-12">
                                <div class="card">
                                    <div class="card-header" style="background-color:#a90329">
                                        <h4 class="title">Correo </h4>
                                        <p class="category">Previsualizacion del correo para: {{fact.cliente}}</p>
                                    </div>
                                    <div class="card-content table-responsive">
                                        <input type="text" id="factRfc" value="{{fact.rfc}}" style="display:none;" />
                                        <div class="form-group">
                                            <label for="fpara" class="col-sm-1 control-label">Correo:</label>
                                            <div class="col-sm-11">
                                                <span class="tags" data-ng-repeat="item in fact.correos track by $index" id="mails">
                                                    <span class="label label-info">
                                                        {{item.Correo}}
                                                        <i class="fa fa-times" style="cursor:pointer" data-ng-click="fact.quitarMail($index)"></i>
                                                    </span>
                                                </span>&hellip;
                                            </div>
                                        </div>
                                        <br />
                                        <input type="email" class="form-control" id="cCorreos" placeholder="Presione Enter o espacio para agregar un nuevo correo" value="{{fact.nuevo}}" data-ng-model="fact.nuevo" data-ng-keypress="fact.keyMail($event)" />
                                        <br />
                                        <div class="form-group">
                                            <label for="fAsunto" class="col-sm-1 control-label">Asunto :</label>
                                            <div class="col-sm-11">
                                                <input type="text" class="form-control" id="fAsunto" placeholder="Asunto" value="{{fact.asunto}}" data-ng-model="fact.asunto" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label for="fCuerpo" class="col-sm-1 control-label">Cuerpo :</label>
                                            <div class="col-sm-11">
                                                <textarea id="correos" name="correos"></textarea>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label for="fpara" class="col-sm-1 control-label">Archivos Adjuntos :</label>
                                            <div class="col-sm-11">
                                                <span class="tags" data-ng-repeat="item in fact.archivo track by $index" id="archivos">
                                                    <span class="label label-info">
                                                        {{item.NumeroReporte}}
                                                        <i class="fa fa-times" style="cursor:pointer" data-ng-click="fact.quitarMail($index)"></i>
                                                    </span>
                                                </span>&hellip;
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label for="cAdjunto" class="col-sm-1 control-label">Nuevo Archivo Adjunto :</label>
                                            <div class="col-sm-10">
                                                <form id="fileupload" action="webServices.aspx/" method="POST" enctype="multipart/form-data" class="dropzone" >
                                                    <div class="dz-message needsclick">Seleccione archivos .XML o .PDF</div>
												</form>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-10">
                                                <button type="submit" id="factRegresar" class="btn btn-default" data-ng-click="fact.regresar()">Regresar</button>
                                                <button type="submit" id="factEnviar" class="btn btn-default" data-ng-click="fact.enviar()">Enviar Correo</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Inicio Footer-->
    <footer class="footer">
        <div class="container-fluid">
            <nav class="pull-left">
                <ul>
                    <li>
                        <a href="#">Inicio</a>
                    </li>
                </ul>
            </nav>
            <p class="copyright pull-right">
                Desarrollado por: <a href="http://acesa.me/">Acesa IT Services</a>
                &copy;
                <script>document.write(new Date().getFullYear());</script>
            </p>
        </div>
    </footer>

    <!-- Angular Acesa-->
    <script src="assets/js/angular.min.js"></script>
    <script src="assets/js/angular-resource.min.js"></script>
	
    <!--   Core JS Files   -->
    <%--<script src="assets/js/jquery-3.2.1.min.js" type="text/javascript"></script>--%>
    <script src="https://code.jquery.com/jquery-2.0.1.min.js" type="text/javascript"></script>
    <script src="assets/ckeditor/ckeditor.js"></script>
    <script src="assets/ckeditor/samples/js/sample.js"></script>
    <script type="text/javascript">
        CKEDITOR.replace('editor');
        CKEDITOR.replace('nuevo');
        CKEDITOR.replace('correos');
    </script>
    <script src="assets/js/index.js"></script>
    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/js/material.min.js" type="text/javascript"></script>
    <!--  Charts Plugin -->
    <script src="assets/js/chartist.min.js"></script>
    <!--  DropzoneJS plugin-->
    <script src="assets/js/dropzone.js"></script>
    <!--  Dynamic Elements plugin -->
    <script src="assets/js/arrive.min.js"></script>
    <!--  Perfectr llorarollbar Library -->
    <script src="assets/js/perfect-scrollbar.jquery.min.js"></script>
    <!--  Notifications Plugin    -->
    <script src="assets/js/bootstrap-notify.js"></script>
    <!-- Material Dashboard javascript methods -->
    <script src="assets/js/material-dashboard.js"></script>
	
	    <script type="text/javascript">
            $(document).ready(function () {

                function listCorreos() {
                    var cor = $("span.tags#mails").find("span"), list = new Array();
                    cor.each(function () {
                        list.push($(this).text().replace("\n", '').trim());
                    });
                    return JSON.stringify(list);
                }

                function listArchivos() {
                    var cor = $("span.tags#archivos").find("span"), list = new Array();
                    cor.each(function () {
                        list.push($(this).text().replace("\n", '').trim());
                    });
                    return JSON.stringify(list);
                }

                $("button#factEnviar").click(function () {
                    console.log("en el boton");
                    var myDropzone = Dropzone.forElement(".dropzone");
                    console.log(myDropzone);
                    myDropzone.processQueue();
                    console.log(myDropzone);
                });

                Dropzone.options.fileupload = {
                    url: 'webServices.aspx/'
                    , paramName: 'file'
                    , acceptedFiles: ".pdf,.PDF,.xml,.XML"
                    , autoProcessQueue: false
                    , uploadMultiple: true
                    , maxFilesize: 10 //MB
                    , maxFiles: 10//archivos que puede manejar al mismo tiempo
                    , createImageThumbnails: false
                    , autoQueue: true
                    , init: function () {
                        myDropzone = this;
                        myDropzone.on("sending", function (file, xhr, formData) {
                            formData.append("rfc", $("#factRfc").val());
                            formData.append("correos", listCorreos());
                            formData.append("asunto", "[" + $("#fAsunto").val() + "]");
                            formData.append("cuerpo", "["+CKEDITOR.instances.correos.getData().replace(/<[^>]*>?/g, '')+"]");
                            formData.append("archivos", listArchivos());
                        });
                        myDropzone.on("success", function (file) {
                            $("a[href=#factNoEnviadas]").click();
                            $.notify({ title: "Listo", message: "Se envio el correo correctamente" }, { type: "success" });
                        });
                        myDropzone.on("error", function (file) {
                            console.log(file);
                            alert(file);
                            $.notify({ title: "Advertencia", message: "Existio un error intentelo mas tarde" }, { type: "danger", delay: 60000 });
                        });
                    }
                };

            });
    </script>

</body>         
</html>


