namespace MacapSoftCAPUAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Barrios",
                c => new
                    {
                        idBarrio = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        nombre = c.String(unicode: false),
                        localidades_idLocalidad = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idBarrio)
                .ForeignKey("dbo.Localidades", t => t.localidades_idLocalidad)
                .Index(t => t.localidades_idLocalidad);
            
            CreateTable(
                "dbo.Localidades",
                c => new
                    {
                        idLocalidad = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        nombre = c.String(unicode: false),
                        ciudad_idCiudad = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idLocalidad)
                .ForeignKey("dbo.Ciudades", t => t.ciudad_idCiudad)
                .Index(t => t.ciudad_idCiudad);
            
            CreateTable(
                "dbo.Ciudades",
                c => new
                    {
                        idCiudad = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        nombre = c.String(unicode: false),
                        pais_idPais = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idCiudad)
                .ForeignKey("dbo.Paises", t => t.pais_idPais)
                .Index(t => t.pais_idPais);
            
            CreateTable(
                "dbo.Paises",
                c => new
                    {
                        idPais = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        nombrePais = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.idPais);
            
            CreateTable(
                "dbo.Consultante",
                c => new
                    {
                        cedula = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        nombre = c.String(unicode: false),
                        apellido = c.String(unicode: false),
                        telefono = c.String(unicode: false),
                        parentezco = c.String(unicode: false),
                        tipoDocumento_idDocumento = c.Long(),
                    })
                .PrimaryKey(t => t.cedula)
                .ForeignKey("dbo.tipos_documentos", t => t.tipoDocumento_idDocumento)
                .Index(t => t.tipoDocumento_idDocumento);
            
            CreateTable(
                "dbo.tipos_documentos",
                c => new
                    {
                        idDocumento = c.Long(nullable: false, identity: true),
                        tipo = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.idDocumento);
            
            CreateTable(
                "dbo.consultantePaciente",
                c => new
                    {
                        idConsultantePaciente = c.Int(nullable: false, identity: true),
                        idConsultante_cedula = c.String(maxLength: 128, storeType: "nvarchar"),
                        idPaciente_numeroDocumento = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idConsultantePaciente)
                .ForeignKey("dbo.Consultante", t => t.idConsultante_cedula)
                .ForeignKey("dbo.Paciente", t => t.idPaciente_numeroDocumento)
                .Index(t => t.idConsultante_cedula)
                .Index(t => t.idPaciente_numeroDocumento);
            
            CreateTable(
                "dbo.Paciente",
                c => new
                    {
                        numeroDocumento = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        nombre = c.String(unicode: false),
                        apellido = c.String(unicode: false),
                        sexo = c.String(unicode: false),
                        fechaNacimiento = c.DateTime(precision: 0),
                        lugarNacimiento = c.String(unicode: false),
                        edad = c.Int(nullable: false),
                        direccion = c.String(unicode: false),
                        telefono = c.String(unicode: false),
                        email = c.String(unicode: false),
                        ocupacion = c.String(unicode: false),
                        activo = c.String(unicode: false),
                        antecedentes = c.String(unicode: false),
                        estadoCivil = c.String(unicode: false),
                        religion = c.String(unicode: false),
                        idUser = c.String(unicode: false),
                        problematicaActual = c.String(unicode: false),
                        historiaPersonal = c.String(unicode: false),
                        historiaFamiliar = c.String(unicode: false),
                        genograma = c.String(unicode: false),
                        barrio_idBarrio = c.String(maxLength: 128, storeType: "nvarchar"),
                        eps_IdEPS = c.String(maxLength: 128, storeType: "nvarchar"),
                        estrato_idEstrato = c.Int(),
                        localidad_idLocalidad = c.String(maxLength: 128, storeType: "nvarchar"),
                        nivelEscolaridad_idNivelEscolaridad = c.Int(),
                        profesion_idProfesion = c.Int(),
                        tipoDocumento_idDocumento = c.Long(),
                    })
                .PrimaryKey(t => t.numeroDocumento)
                .ForeignKey("dbo.Barrios", t => t.barrio_idBarrio)
                .ForeignKey("dbo.Eps", t => t.eps_IdEPS)
                .ForeignKey("dbo.estrato", t => t.estrato_idEstrato)
                .ForeignKey("dbo.Localidades", t => t.localidad_idLocalidad)
                .ForeignKey("dbo.nivelEscolaridad", t => t.nivelEscolaridad_idNivelEscolaridad)
                .ForeignKey("dbo.profesion", t => t.profesion_idProfesion)
                .ForeignKey("dbo.tipos_documentos", t => t.tipoDocumento_idDocumento)
                .Index(t => t.barrio_idBarrio)
                .Index(t => t.eps_IdEPS)
                .Index(t => t.estrato_idEstrato)
                .Index(t => t.localidad_idLocalidad)
                .Index(t => t.nivelEscolaridad_idNivelEscolaridad)
                .Index(t => t.profesion_idProfesion)
                .Index(t => t.tipoDocumento_idDocumento);
            
            CreateTable(
                "dbo.Eps",
                c => new
                    {
                        IdEPS = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        nombre = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.IdEPS);
            
            CreateTable(
                "dbo.estrato",
                c => new
                    {
                        idEstrato = c.Int(nullable: false, identity: true),
                        numeroEstrato = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.idEstrato);
            
            CreateTable(
                "dbo.nivelEscolaridad",
                c => new
                    {
                        idNivelEscolaridad = c.Int(nullable: false, identity: true),
                        nombre = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.idNivelEscolaridad);
            
            CreateTable(
                "dbo.profesion",
                c => new
                    {
                        idProfesion = c.Int(nullable: false, identity: true),
                        nombre = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.idProfesion);
            
            CreateTable(
                "dbo.Diagnosticos",
                c => new
                    {
                        Codigo = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Nombre = c.String(unicode: false),
                        Destacado = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.inasistencias",
                c => new
                    {
                        idInasistencias = c.Int(nullable: false, identity: true),
                        motivo = c.String(unicode: false),
                        fechaInasistencia = c.DateTime(precision: 0),
                        usuario = c.String(unicode: false),
                        paciente_numeroDocumento = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idInasistencias)
                .ForeignKey("dbo.Paciente", t => t.paciente_numeroDocumento)
                .Index(t => t.paciente_numeroDocumento);
            
            CreateTable(
                "dbo.Remisiones",
                c => new
                    {
                        idRemision = c.Int(nullable: false, identity: true),
                        nombreInsitucionRemitida = c.String(unicode: false),
                        direccion = c.String(unicode: false),
                        telefono = c.String(unicode: false),
                        nombreProfesional = c.String(unicode: false),
                        accionesEmprendidas = c.String(unicode: false),
                        expectativasServicio = c.String(unicode: false),
                        fechaRemitido = c.DateTime(nullable: false, precision: 0),
                        usuario = c.String(unicode: false),
                        programacionPsicologo = c.String(unicode: false),
                        anexos = c.String(unicode: false),
                        listaEspera = c.String(unicode: false),
                        remitido = c.String(unicode: false),
                        personalizado = c.String(unicode: false),
                        diagnostico_Codigo = c.String(maxLength: 128, storeType: "nvarchar"),
                        motivoRemision_idMotivoRemision = c.Int(),
                        paciente_numeroDocumento = c.String(maxLength: 128, storeType: "nvarchar"),
                        serviSoli_idServicioSolicitado = c.Int(),
                    })
                .PrimaryKey(t => t.idRemision)
                .ForeignKey("dbo.Diagnosticos", t => t.diagnostico_Codigo)
                .ForeignKey("dbo.motivosRemisiones", t => t.motivoRemision_idMotivoRemision)
                .ForeignKey("dbo.Paciente", t => t.paciente_numeroDocumento)
                .ForeignKey("dbo.servicioSolicitado", t => t.serviSoli_idServicioSolicitado)
                .Index(t => t.diagnostico_Codigo)
                .Index(t => t.motivoRemision_idMotivoRemision)
                .Index(t => t.paciente_numeroDocumento)
                .Index(t => t.serviSoli_idServicioSolicitado);
            
            CreateTable(
                "dbo.motivosRemisiones",
                c => new
                    {
                        idMotivoRemision = c.Int(nullable: false, identity: true),
                        nombre = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.idMotivoRemision);
            
            CreateTable(
                "dbo.servicioSolicitado",
                c => new
                    {
                        idServicioSolicitado = c.Int(nullable: false, identity: true),
                        nombre = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.idServicioSolicitado);
            
            CreateTable(
                "dbo.EntidadRemitente",
                c => new
                    {
                        idRemitente = c.Int(nullable: false, identity: true),
                        nombreEntidad = c.String(unicode: false),
                        fechaRemision = c.DateTime(nullable: false, precision: 0),
                        nombreRemitente = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.idRemitente);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Activo = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Remisiones", "serviSoli_idServicioSolicitado", "dbo.servicioSolicitado");
            DropForeignKey("dbo.Remisiones", "paciente_numeroDocumento", "dbo.Paciente");
            DropForeignKey("dbo.Remisiones", "motivoRemision_idMotivoRemision", "dbo.motivosRemisiones");
            DropForeignKey("dbo.Remisiones", "diagnostico_Codigo", "dbo.Diagnosticos");
            DropForeignKey("dbo.inasistencias", "paciente_numeroDocumento", "dbo.Paciente");
            DropForeignKey("dbo.consultantePaciente", "idPaciente_numeroDocumento", "dbo.Paciente");
            DropForeignKey("dbo.Paciente", "tipoDocumento_idDocumento", "dbo.tipos_documentos");
            DropForeignKey("dbo.Paciente", "profesion_idProfesion", "dbo.profesion");
            DropForeignKey("dbo.Paciente", "nivelEscolaridad_idNivelEscolaridad", "dbo.nivelEscolaridad");
            DropForeignKey("dbo.Paciente", "localidad_idLocalidad", "dbo.Localidades");
            DropForeignKey("dbo.Paciente", "estrato_idEstrato", "dbo.estrato");
            DropForeignKey("dbo.Paciente", "eps_IdEPS", "dbo.Eps");
            DropForeignKey("dbo.Paciente", "barrio_idBarrio", "dbo.Barrios");
            DropForeignKey("dbo.consultantePaciente", "idConsultante_cedula", "dbo.Consultante");
            DropForeignKey("dbo.Consultante", "tipoDocumento_idDocumento", "dbo.tipos_documentos");
            DropForeignKey("dbo.Barrios", "localidades_idLocalidad", "dbo.Localidades");
            DropForeignKey("dbo.Localidades", "ciudad_idCiudad", "dbo.Ciudades");
            DropForeignKey("dbo.Ciudades", "pais_idPais", "dbo.Paises");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Remisiones", new[] { "serviSoli_idServicioSolicitado" });
            DropIndex("dbo.Remisiones", new[] { "paciente_numeroDocumento" });
            DropIndex("dbo.Remisiones", new[] { "motivoRemision_idMotivoRemision" });
            DropIndex("dbo.Remisiones", new[] { "diagnostico_Codigo" });
            DropIndex("dbo.inasistencias", new[] { "paciente_numeroDocumento" });
            DropIndex("dbo.Paciente", new[] { "tipoDocumento_idDocumento" });
            DropIndex("dbo.Paciente", new[] { "profesion_idProfesion" });
            DropIndex("dbo.Paciente", new[] { "nivelEscolaridad_idNivelEscolaridad" });
            DropIndex("dbo.Paciente", new[] { "localidad_idLocalidad" });
            DropIndex("dbo.Paciente", new[] { "estrato_idEstrato" });
            DropIndex("dbo.Paciente", new[] { "eps_IdEPS" });
            DropIndex("dbo.Paciente", new[] { "barrio_idBarrio" });
            DropIndex("dbo.consultantePaciente", new[] { "idPaciente_numeroDocumento" });
            DropIndex("dbo.consultantePaciente", new[] { "idConsultante_cedula" });
            DropIndex("dbo.Consultante", new[] { "tipoDocumento_idDocumento" });
            DropIndex("dbo.Ciudades", new[] { "pais_idPais" });
            DropIndex("dbo.Localidades", new[] { "ciudad_idCiudad" });
            DropIndex("dbo.Barrios", new[] { "localidades_idLocalidad" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EntidadRemitente");
            DropTable("dbo.servicioSolicitado");
            DropTable("dbo.motivosRemisiones");
            DropTable("dbo.Remisiones");
            DropTable("dbo.inasistencias");
            DropTable("dbo.Diagnosticos");
            DropTable("dbo.profesion");
            DropTable("dbo.nivelEscolaridad");
            DropTable("dbo.estrato");
            DropTable("dbo.Eps");
            DropTable("dbo.Paciente");
            DropTable("dbo.consultantePaciente");
            DropTable("dbo.tipos_documentos");
            DropTable("dbo.Consultante");
            DropTable("dbo.Paises");
            DropTable("dbo.Ciudades");
            DropTable("dbo.Localidades");
            DropTable("dbo.Barrios");
        }
    }
}
