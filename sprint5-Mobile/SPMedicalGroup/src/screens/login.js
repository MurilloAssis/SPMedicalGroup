import React, { Component } from 'react'

import AsyncStorage from '@react-native-async-storage/async-storage';

import {
    StyleSheet,
    ImageBackground,
    View,
    Image,
    TextInput,
    Text,
    TouchableOpacity
} from 'react-native'

import api from '../services/api'

export default class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            senha: ""
        }
    }

    realizarLogin = async () => {
        console.warn(this.state.email + this.state.senha)
        const resposta = await api.post('/Login', {
            emailUsuario: this.state.email,
            senhaUsuario: this.state.senha
        })
        console.warn(resposta)

        const token = resposta.data.token


        await AsyncStorage.setItem('userToken', token)

        if (resposta.status == 200) {
            console.warn('Login efetuado')
            this.props.navigation.navigate('Main')
        }
    };

    render() {
        return (
            <ImageBackground
                source={require('../assets/Fundo_Login.png')}
                style={styles.overlay}
            >
                <View style={styles.main}>
                    <Image
                        source={require('../assets/logo.png')}
                        style={styles.logo}
                    />
                    <Text style={styles.warnText}>Para continuar, faça o login</Text>
                    <View style={styles.form}>

                        <TextInput
                            placeholder="Email"
                            textContentType='emailAddress'
                            style={styles.inputText}
                            onChangeText={email => this.setState({email})}
                        />
                        <TextInput
                            placeholder="Senha"
                            secureTextEntry={true}
                            style={styles.inputText}
                            onChangeText={senha => this.setState({senha})}
                        />
                        <TouchableOpacity
                            onPress={this.realizarLogin}
                            style={styles.btnLogin}
                        >
                            <Text style={styles.txtBtnLogin}>Login</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </ImageBackground>
        )
    }
}



const styles = StyleSheet.create({

    overlay: {
        ...StyleSheet.absoluteFillObject,
        alignItems: 'center',
        justifyContent: 'center'

    },

    main: {
        width: 280,
        height: 440,
        backgroundColor: '#fff',
        alignItems: 'center'

    },
    logo: {
        marginTop: 30,
        marginBottom: 30,


    },
    warnText:{
        color: '#000'
    },
    form: {
        marginTop: 30,
        marginBottom: 30
    },
    inputText:{
        width: 180,
        alignItems: 'flex-start',
        marginBottom: 5 
    },
    btnLogin:{
        backgroundColor: '#55B1DB',
        height: 30,
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 30
    },
    txtBtnLogin:{
        color: '#fff'
    }
})