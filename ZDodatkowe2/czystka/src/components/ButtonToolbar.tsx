import React from "react";
import styled from "styled-components";

const Toolbar = styled.div`
    display: flex;
    justify-content: space-between;
    flex-wrap: wrap;
    padding: 40px;
`;

const Button = styled.button`
    color: #fff;
    text-transform: uppercase;
    text-decoration: none;
    background: #ed3330;
    padding: 20px;
    border-radius: 5px;
    display: inline-block;
    border: none;
    margin: 10px;
    transition: all 0.4s ease 0s;

    &:hover {
        background: #434343;
        -webkit-box-shadow: 0px 5px 40px -10px rgba(0,0,0,0.57);
        -moz-box-shadow: 0px 5px 40px -10px rgba(0,0,0,0.57);
        box-shadow: 5px 40px -10px rgba(0,0,0,0.57);
        transition: all 0.4s ease 0s;
    }
`;

interface ButtonAttributes {
    "btnName": string,
    "goto": number,
    "addThing"?: string,
    "addEvent"?: string,
    "removeThing"?: string,
    "removeEvent"?: string,
    "addChances"?: number,
    "missionStart"?: number
}

interface ButtonToolbarProps {
    buttons?: ButtonAttributes[];
    updateId: (id: number, recentChances: number) => void;
    addChances: (addValue: number) => void;
    items: Set<string>;
    events: Set<string>;
    updateUserElements: (items: Set<string>, events: Set<string>) => void;
}

const ButtonToolbar = (props: ButtonToolbarProps) => {

    const onClick = (button: ButtonAttributes) => {
        const items = props.items;
        const events = props.events;
        button.missionStart && props.addChances(0);
        button.addThing && items.add(button.addThing);
        button.removeThing && items.delete(button.removeThing);
        button.addEvent && events.add(button.addEvent);
        button.removeEvent && events.delete(button.removeEvent);
        if (button.removeThing && button.removeThing === "ALL") items.clear();
        if (button.removeEvent && button.removeEvent === "ALL") events.clear();
        props.updateUserElements(items, events);
        if (button.addChances) {
            props.addChances(button.addChances);
            props.updateId(button.goto, button.addChances);
        } else {
            props.updateId(button.goto, 0);
        }
    };

    const Buttons = props.buttons?.map( (button) => {
        return <Button onClick={() => onClick(button)}> {button.btnName}</Button>
    });

    return (
        <Toolbar>
            {Buttons}
        </Toolbar>
    )
};


export default ButtonToolbar;