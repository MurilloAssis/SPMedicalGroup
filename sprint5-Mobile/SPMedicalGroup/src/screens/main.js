import React, { Component } from 'react'
import {
    Image,
    StatusBar,
    StyleSheet,
    View
} from 'react-native'

import { createBottomTabNavigator } from '@react-navigation/bottom-tabs'

const bottomTab = createBottomTabNavigator();

import Consultas from './Consultas';

export default class Main extends Component {
    render() {
        return (
            <View style={styles.main}>
                <StatusBar
                    hidden={false}
                />
                <bottomTab.Navigator
                    initialRouteName='Consultas'
                    screenOptions={({ route }) => ({
                        tabBarIcon: () => {
                            if (route.name === 'Consultas') {
                                return (
                                    <Image
                                        source={require('../assets/checklist.png')}
                                        style={styles.tabBarIcon}
                                    />
                                )
                            }
                        },
                        headerShown: false,
                        tabBarShowLabel: false,
                        tabBarActiveBackgroundColor: '#1EE7BE',
                        tabBarInactiveBackgroundColor: '#DD99FF',
                        tabBarStyle: { height: 50 }
                    })}
                >


                <bottomTab.Screen name="Consultas" component={Consultas}/>

                </bottomTab.Navigator>


            </View>
        )
    }
}

const styles = StyleSheet.create({
    // conteúdo da main
    main: {
        flex: 1,
        backgroundColor: '#1EE7BE'
    },
    // estilo dos ícones da tabBar
    tabBarIcon: {
        width: 22,
        height: 22,
        
    }
});