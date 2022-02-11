import * as Yup from 'yup';

export const schemaLongUrl = Yup.object().shape({
    url: Yup.string().max(80000).required().matches(/https?:\/\/.*/gmi)
});