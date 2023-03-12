import React, { Component } from 'react';

import api from '../services/api';

import { TouchableOpacity } from 'react-native-gesture-handler';

import AsyncStorage from '@react-native-async-storage/async-storage';
import { View, Text, StyleSheet, FlatList } from 'react-native';

export default class Consultas extends Component {
    constructor(props) {
        super(props);
        this.state = {
            listaConsultas: []
        };
    }

    buscarConsultas = async () => {
        const xambers = await AsyncStorage.getItem('userToken')


        const resposta = await api.get('/Consultas/Lista/Minhas', {
            headers: {
                authorization: 'Bearer ' + xambers
            }
        });

        const dadosDaApi = resposta.data;

        this.setState({ listaConsultas: dadosDaApi })

    }
    componentDidMount() {
        this.buscarConsultas();
    }

    render() {
        return (
            <View style={styles.main}>
                <View style={styles.containerTitle}>
                    <Text style={styles.title}>Minhas Consultas</Text>
                </View>

                <View style={styles.containerList}>
                    <FlatList
                        contentContainerStyle={styles.mainBodyList}
                        data={this.state.listaConsultas}
                        keyExtractor={item => item.idConsulta}
                        renderItem={this.renderItem}
                    />
                </View>
            </View>

        )
    }

    renderItem = ({ item }) => (
        <View style={styles.flatItemRow}>
            <View style={styles.flatItemContentRow}>
                <Text style={styles.itemTitle}>Nome do paciente:</Text>
                <Text style={styles.flatItemContent}>{item.idPacienteNavigation.idUsuarioNavigation.nomeUsuario}</Text>
            </View>

            <View style={styles.flatItemContentRow}>
                <Text style={styles.itemTitle}>Nome do Médico:</Text>
                <Text style={styles.flatItemContent}>{item.idMedicoNavigation.idUsuarioNavigation.nomeUsuario}</Text>
            </View>

            <View style={styles.flatItemContentRow}>
                <Text style={styles.itemTitle}>Data da Consulta:</Text>
                <Text style={styles.flatItemContent}>{Intl.DateTimeFormat("pt-BR",{
                    year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric'
                }).format(new Date(item.dataConsulta))}</Text>
            </View>
            <View style={styles.flatItemContentRow}>
                <Text style={styles.itemTitle}>Descrição da Consulta:</Text>
                <Text style={styles.flatItemContent}>{item.descricaoConsulta}</Text>
            </View>
            
        </View>
    )
}

const styles = StyleSheet.create({
    main:{
        flex: 1
    },
    mainBodyList:{
        
    },
    containerTitle: {
       height: 80,
        alignItems: 'center',
        justifyContent: 'center',

    },
    title: {
        fontFamily: 'Roboto-Black',
        fontSize: 23,
        color: '#000'
    },
    containerList: {
        alignItems: 'center',
        
    },
    flatItemRow: {
        borderRadius: 20,
        borderColor: '#000',
        borderWidth: 2,
        width: 350,
        height: 140,
        padding: 10,
        marginBottom: 90,
        justifyContent:'center'

    },
    flatItemContentRow:{
        flexDirection: 'row',
        justifyContent: 'space-evenly',
        

    },

    itemTitle: {
        fontWeight: 'bold',
        color: '#000'
        
    }
})