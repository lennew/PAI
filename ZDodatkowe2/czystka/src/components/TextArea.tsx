import React from "react";
import styled from "styled-components";
import { Text } from "react-native";

const Area = styled.div`
    display: flex;
    width: 75%;
    padding: 15px;
    border: 8px groove #551838;
    border-radius: 18px;
    flexDirection: 'column';
`;

interface TextAreaProps {
    text: string
}

const TextArea = (props: TextAreaProps) => {
    return (
        <Area>
            <Text style={{fontSize: 20}}>
                {props.text}
            </Text>
        </Area>
    )
};


export default TextArea;