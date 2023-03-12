import { useEffect, useState } from "react"
import '../../assets/css/style.css'
import '../../assets/css/consultasAdm.css'
import Header from "../../components/Header/header"
import Footer from "../../components/Footer/footer"
import axios from "axios"

export default function Medico(){
    const [ listaConsultas, setListaConsultas ] = useState( [] )

    function consultasMedico(){
        axios.get('http://localhost:5000/api/Consultas/Lista/Minhas', {
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

    useEffect(consultasMedico, [])
    return(
        <div>
            <Header></Header>
            <main className='conteudoPrincipal'>
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
            </main>
            <Footer></Footer>
        </div>
    )
}