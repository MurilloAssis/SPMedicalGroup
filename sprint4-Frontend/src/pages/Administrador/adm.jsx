import { useEffect, useState } from "react"
import '../../assets/css/style.css'
import '../../assets/css/consultasAdm.css'
import Header from "../../components/Header/header"
import Footer from "../../components/Footer/footer"
import axios from "axios"

export default function Administrador(){

    const [ listaConsultas, setListaConsultas ] = useState( [] )
    const [listaMedicos, setListaMedicos] = useState([])
    const [listaPacientes, setListaPacientes] = useState([])
    const [idMedico, setIdMedico] = useState(0)
    const [idPaciente, setIdPaciente] = useState(0)
    const [dataCadastro, setDataCadastro] = useState(new Date())
    const [isLoading, setIsLoading] = useState(false)

    function consultasAdm(){
        axios.get('http://localhost:5000/api/Consultas/', {
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
            }
        } )

        .then(response => {
            if(response.status === 200){
                setListaConsultas(response.data)
            }
        })
        .catch(erro => console.log(erro))
    }
    
    useEffect(consultasAdm, [])
    
    function medicos() {
        axios('http://localhost:5000/api/Medicos/', {
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
            }
        } )

        .then(response => {
            if (response.status === 200) {
                setListaMedicos(response.data)
            }
        })
        .catch(erro => console.log(erro))
    }
    
    useEffect(medicos, [])
    
    function pacientes() {
        axios('http://localhost:5000/api/Pacientes/', {
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
            }
        } )

        .then(response => {
            if(response.status === 200) {
                setListaPacientes(response.data)
            }
        })
        .catch(erro => console.log(erro))
    }

    useEffect(pacientes, [])


    

    function cadastrarConsulta(event) {
        event.preventDefault();

        

        setIsLoading(true)
        
        axios.post("http://localhost:5000/api/Consultas",{
            IdMedico : idMedico,
            idPaciente : idPaciente,
            dataConsulta : dataCadastro
        },{
            
            headers : {
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
            }
            
            
            
        } )
        .then(response => {
            if (response.status === 201) {
                setIsLoading(false)
                consultasAdm()
            }
        })
        .catch(erro => console.log(erro))
    }
   
        return (
            <div>

            
            <Header></Header>
            <main className="conteudoPrincipal">
                <section className="conteudoPrincipal-cadastro">


                    <section className="container" id="conteudoPrincipal-cadastro">
                        <h2 className="conteudoPrincipal-cadastro-titulo">
                            Cadastrar Consulta
                        </h2>
                        <form onSubmit={cadastrarConsulta}>
                            <div className="container">
                                <select name="medico" onChange={ (evt) => setIdMedico(evt.target.value) } id="">
                                <option value="#">Escolha um médico</option>
                                    {
                                        listaMedicos.map( (event)  => {
                                            return(

                                                <option key={event.idMedico} value={event.idMedico} >{event.idUsuarioNavigation.nomeUsuario} </option>
                                                )
                                        })
                                    }
                                    
                                </select>

                                <select name="paciente" id=""  onChange={(evt) => setIdPaciente(evt.target.value)}>
                                <option value="#">Escolha um paciente</option>
                                    {
                                        listaPacientes.map( (event)  => {

                                            return(

                                                <option key={event.idPaciente} value={event.idPaciente}>{event.idUsuarioNavigation.nomeUsuario}</option>
                                                )
                                        })
                                    }
                                </select>

                                <input onChange={(evt) => setDataCadastro(evt.target.value)} type="datetime-local" />

                                {isLoading && (
                                    <button
                                    type="submit"
                                    className="conteudoPrincipal-btn conteudoPrincipal-btn-cadastro"
                                    disabled
                                >
                                    Carregando...
                                </button>
                                )}

                                {isLoading === false && (
                                    <button
                                    type="submit"
                                    className="conteudoPrincipal-btn conteudoPrincipal-btn-cadastro"
                                >
                                    Cadastrar
                                </button>
                                )}
                                
                            </div>
                        </form>
                    </section>

                    <h2 className="conteudoPrincipal-cadastro-titulo">Consultas</h2>
                    <div className="container" id="conteudoPrincipal-lista">
                        <table id="tabela-lista">
                            <thead>
                                <tr>
                                    <th>Descrição</th>
                                    <th>Paciente</th>
                                    <th>Médico</th>
                                    <th>Data</th>
                                    <th>Instituição</th>
                                </tr>
                            </thead>

                            <tbody id="tabela-lista-corpo">
                                {

                                    listaConsultas.map( (event)  => {
                                        return(

                                            <tr key={event.idConsulta}>
                                            <td>{event.descricaoConsulta}</td>
                                            <td>{event.idPacienteNavigation.idUsuarioNavigation.nomeUsuario}</td>
                                            <td>{event.idMedicoNavigation.idUsuarioNavigation.nomeUsuario}</td>
                                            <td>
                                                {Intl.DateTimeFormat("pt-BR", {
                                                    year: 'numeric', month: 'short', day: 'numeric'
                                                }).format(new Date(event.dataConsulta))}
                                                </td>
                                            <td>{event.idMedicoNavigation.idInstituicaoNavigation.nomeFantasia}</td>
                                        </tr>
                                        )
                                    })
                                }
                            </tbody>
                        </table>
                    </div>
                </section>
            </main>
            <Footer></Footer>
            </div>
        )
    
}